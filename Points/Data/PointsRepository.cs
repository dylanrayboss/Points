using Microsoft.EntityFrameworkCore;
using Points.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Points.Data
{
    public class PointsRepository : IPointsRepository
    {
        private readonly DataContext context;

        public PointsRepository(DataContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public async Task<Transaction> GetTransaction(int id)
        {
            var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == id);

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            var transactions = await context.Transactions.ToListAsync();

            return transactions;
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }

    }
}
