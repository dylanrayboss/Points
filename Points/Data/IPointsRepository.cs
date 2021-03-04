using Points.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Points.Data
{
    public interface IPointsRepository
    {
        void Add<T>(T entity) where T : class;
        Task<Transaction> GetTransaction(int id);
        Task<IEnumerable<Transaction>> GetTransactions();
        Task<bool> SaveAll();
    }
}