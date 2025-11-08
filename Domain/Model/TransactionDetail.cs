using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class TransactionDetail
    {
        public long Id { get; set; }
        public long TransactionId { get; set; }
        public long ProductId { get; set; }
        public char stsrc { get; set; } = 'A';
        public DateTime? date_created { get; set; } = DateTime.Now;
        public string? created_by { get; set; } = "Admin";
        public DateTime? date_updated { get; set; }
        public string? updated_by { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }
        //[ForeignKey("ProductId")]
        //public virtual Product Product { get; set; }
    }
}
