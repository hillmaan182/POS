using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductType { get; set; }
        public string? ProductBrand { get; set; }
        public string? ProductDescription { get; set; }
        public int? Stock { get; set; }
    }
}
