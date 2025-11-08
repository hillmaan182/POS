using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class UserModel 
    {
        [Key]
        public string? Username { get; set; }
        public string Password { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime DateOfJoin { get; set; }
    }
}
