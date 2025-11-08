using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class MasterRef
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? RefName { get; set; }
        public string? RefCode { get; set; }
        public string? RefValue { get; set; }
        public DateTime? RefLastDate { get; set; }
        public string? RefLastValue { get; set; }
        public int? RefLastCounter { get; set; }
    }
}
