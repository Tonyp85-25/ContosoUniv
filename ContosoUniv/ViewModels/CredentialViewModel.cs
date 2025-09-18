using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniv.ViewModels;

public class CredentialViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name= "Email address")]
    public string EmailAddress { get; set; }

    [Required]
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}