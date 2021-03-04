using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Points.Data;
using Points.Dtos;
using Points.Models;

namespace Points.Controllers
{
    [ApiController]
    [Route("api")]
    public class PointsController : ControllerBase
    {
        private readonly IPointsRepository repository;
        private readonly IMapper mapper;

        public PointsController(IPointsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await repository.GetTransactions();

            var transactionDictionary = mapper.Map<Dictionary<string, int>>(transactions);

            return Ok(transactionDictionary);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] string transaction)
        {
            if (string.IsNullOrEmpty(transaction))
            {
                return BadRequest(new ErrorDto
                {
                    Error = "Please specify JSON in the body of the request."
                });
            }

            TransactionDto transactionDto;
            try
            {
                transactionDto = JsonConvert.DeserializeObject<TransactionDto>(transaction);
            }
            catch (JsonException exception)
            {
                return BadRequest(new ErrorDto
                {
                    Error = exception.Message
                });
            }

            var transactionToInsert = mapper.Map<Transaction>(transactionDto);

            repository.Add(transactionToInsert);

            if (await repository.SaveAll())
            {
                return NoContent();
            }

            return Conflict(new
            {
                Error = "Failed to add transaction."
            });
        }

        [HttpPut]
        public async Task<IActionResult> SpendPoints([FromBody] string points)
        {
            if (string.IsNullOrEmpty(points))
            {
                return BadRequest(new ErrorDto
                {
                    Error = "Please specify JSON in the body of the request."
                });
            }

            SpendPointsDto spendPointsDto;
            try
            {
                spendPointsDto = JsonConvert.DeserializeObject<SpendPointsDto>(points);
            }
            catch (JsonException exception)
            {
                return BadRequest(new ErrorDto
                {
                    Error = exception.Message
                });
            }

            if (!spendPointsDto.Points.HasValue || spendPointsDto.Points <= 0)
            {
                return BadRequest(new ErrorDto
                {
                    Error = "Points value must be greater than 0."
                });
            }

            Dictionary<string, int> spentTransactionDictionary = new Dictionary<string, int>();

            var transactions = await repository.GetTransactions();

            var sortedTransactions = transactions.OrderBy(t => t.Timestamp);

            foreach (var transaction in sortedTransactions)
            {
                if (spendPointsDto.Points > 0)
                {
                    if (transaction.Points != 0)
                    {
                        int pointsToRemoveFromTransaction;
                        if (spendPointsDto.Points >= transaction.Points)
                        {
                            pointsToRemoveFromTransaction = transaction.Points;
                        }
                        else
                        {
                            pointsToRemoveFromTransaction = spendPointsDto.Points.Value;
                        }
                        spendPointsDto.Points -= pointsToRemoveFromTransaction;
                        transaction.Points -= pointsToRemoveFromTransaction;

                        if (spentTransactionDictionary.ContainsKey(transaction.Payer))
                        {
                            spentTransactionDictionary[transaction.Payer] += -(pointsToRemoveFromTransaction);
                        }
                        else
                        {
                            spentTransactionDictionary.Add(transaction.Payer, -(pointsToRemoveFromTransaction));
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            if (spendPointsDto.Points == 0)
            {
                var spentTransactionDtos = mapper.Map<IEnumerable<SpentTransactionDto>>(spentTransactionDictionary);
                if (await repository.SaveAll())
                {
                    return Ok(spentTransactionDtos);
                }
            }

            return Conflict(new
            {
                Error = "Your account does not have enough points."
            });
        }
    }
}
