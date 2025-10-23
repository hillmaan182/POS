using Microsoft.EntityFrameworkCore;
using Domain.Model;
namespace Domain.DataContext
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<ImageData> ImageData { get; set; } = default!; 
       

    }
}
