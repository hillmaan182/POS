using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ProductDTO
    {
        [Key]
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
    }
}
