using System.ComponentModel.DataAnnotations;

namespace goolrang_sales_v1.Models;

public class User
{
    [Required]
    public int UserId { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    public string Phone { get; set; }
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; }
}
