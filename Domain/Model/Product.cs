using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Domain.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductType { get; set; }
        public string? ProductDescription { get; set; }
        public string? ImageType { get; set; }
        public string? ProductImage { get; set; }
        public int? ProductPrice { get; set; }
        public char stsrc { get; set; } = 'A';
        public DateTime? date_created { get; set; } = DateTime.Now;
        public string? created_by { get; set; } = "Admin";
        public DateTime? date_updated { get; set; }
        public string? updated_by { get; set; }

        [JsonIgnore]
        public ICollection<OrderDetail>? OrderDetail { get; set; }
        //[JsonIgnore]
        //public virtual Domain.DTO.OrderDetailGetDTO? OrderDetailGetDTO { get; set; }
        //[JsonIgnore]
        //public virtual OrderDetail? OrderDetail { get; set; }
        //public TransactionDetail TransactionDetail { get; set; }
    }
}
