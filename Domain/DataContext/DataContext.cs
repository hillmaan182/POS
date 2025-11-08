using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Domain.JwtAuthModel;

namespace Domain.DataContext
{
    public partial class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Inventory> Inventory { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<OrderDetail> OrderDetail { get; set; } = default!;
        public DbSet<ImageData> ImageData { get; set; } = default!;
        public DbSet<Transaction> Transaction { get; set; } = default!;
        public DbSet<MasterRef> MasterRef { get; set; } = default!;
        public DbSet<TransactionDetail> TransactionDetail { get; set; } = default!;

        public DbSet<UserModel> UserModel { get; set; } = default!;

    }
}
