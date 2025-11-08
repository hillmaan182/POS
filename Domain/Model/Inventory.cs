using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string? InventoryName { get; set; }
        public int? Qty { get; set; }
        public string? QtyDescription { get; set; }
        public int? Price { get; set; }
        public string? InventoryImage { get; set; }
        public char stsrc { get; set; } = 'A';
        public DateTime? date_created { get; set; } = DateTime.Now;
        public string? created_by { get; set; } = "Admin";
        public DateTime? date_updated { get; set; }
        public string? updated_by { get; set; }
    }
}
