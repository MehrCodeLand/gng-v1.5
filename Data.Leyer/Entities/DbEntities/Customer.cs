using System.ComponentModel.DataAnnotations;

namespace goolrang_sales_v1.Models
{
    public class Customer
    {
        [Required]
        public int CutomerId { get; set; }
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? ZipCode { get; set; }
    }
}
