using System.ComponentModel.DataAnnotations;

namespace WebApp.Model;

public class Person
{
    public int UserId { get; set; }
    
    
    [Required(ErrorMessage = "Username is required")]
    [StringLength(maximumLength:20, MinimumLength = 5, ErrorMessage = "Username must be between 3 and 25 characters")]
    [Display(Name = "Username")]
    [RegularExpression( @"^[a-zA-Z0-9]*$", ErrorMessage = "Username must contain only letters and numbers")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(maximumLength:25, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 25 characters")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit and one special character")]
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}