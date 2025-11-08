using Domain.Model;
using Application.Service;
using Domain.DataContext;


namespace Infrastructure.Repository
{
    public class TransactionDetailRepository : GenericRepository<TransactionDetail>, ITransactionDetailRepository
    {
        private readonly DataContext db;
        public TransactionDetailRepository(DataContext _db) : base(_db)
        {
            this.db = _db;
        }
    }
}
