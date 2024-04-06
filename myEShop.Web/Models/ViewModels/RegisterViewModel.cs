using System.ComponentModel.DataAnnotations;

namespace myEShop.Web.Models.ViewModels;

public class RegisterViewModel
{

    [EmailAddress]
    [Required]
    [MaxLength(300)]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required]
    [MaxLength(50)]
    public string Password { get; set; }

    [Required]
    [MaxLength(50)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
