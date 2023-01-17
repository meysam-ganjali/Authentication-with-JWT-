using System.ComponentModel.DataAnnotations;

namespace AuthJWT.Model;

public class UserLogin
{
    [Required]
    public string UserName { get; set; }
    [Required]
    
    public string Password { get; set; }
}