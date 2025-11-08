using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Model;
namespace Domain.DTO
{
    public class OrderDetailGetDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long? orderid { get; set; }
        public long? productid { get; set; }
        public int? qty { get; set; }
        [ForeignKey("orderid")]
        [JsonIgnore]
        public OrderDTO OrderDTO { get; set; }
        [ForeignKey("productid")]
        //[JsonIgnore]
        public Product Product { get; set; }

        
        
        
    }
}
