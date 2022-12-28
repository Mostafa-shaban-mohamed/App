using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

[NotMapped]
public class LoginViewModel{
    public string Email { get; set; }

    public string Password { get; set; }
}