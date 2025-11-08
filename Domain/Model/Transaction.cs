using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Type { get; set; }
        public int? TransactionPrice { get; set; }
        public int? ServiceTax { get; set; }
        public int? ServiceCharge { get; set; }
        public string? Customer { get; set; }
        public int? CustomerNo { get; set; }
        public char stsrc { get; set; } = 'A';
        public DateTime? date_created { get; set; } = DateTime.Now;
        public string? created_by { get; set; } = "Admin";
        public DateTime? date_updated { get; set; }
        public string? updated_by { get; set; }
        public TransactionDetail TransactionDetail { get; set; }
      
    }
}
