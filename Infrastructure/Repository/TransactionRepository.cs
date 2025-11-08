using Domain.Model;
using Application.Service;
using Domain.DataContext;

namespace Infrastructure.Repository
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly DataContext db;
        public TransactionRepository(DataContext _db) : base(_db)
        {
            this.db = _db;
        }
    }
}
