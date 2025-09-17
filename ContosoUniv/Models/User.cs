using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniv.Models;

public enum Role
{
    Employee,
    Administrator
}



public class User
{
    public int Id { get; set; }
    
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
    
    [Required]
    public Role Role { get; set; }
}