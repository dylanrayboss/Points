using AutoMapper;
using Points.Dtos;
using Points.Models;
using System.Collections.Generic;
using System.Linq;

namespace Points.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
            CreateMap<IEnumerable<Transaction>, Dictionary<string, int>>()
                .ConvertUsing(source => source.GroupBy(x => x.Payer).ToDictionary(g => g.Key, g => g.Sum(v => v.Points)));
            CreateMap<Dictionary<string, int>, IEnumerable<SpentTransactionDto>>()
                .ConstructUsing(source => source.Select(d => new SpentTransactionDto
                {
                    Payer = d.Key,
                    Points = d.Value
                }));
        }
    }
}
