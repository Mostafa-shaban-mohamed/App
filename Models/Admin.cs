using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

public class Admin{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string Email { get; set; }
    
    [Column(TypeName = "nvarchar(50)")]
    public string Password { get; set; }
    
    [Column(TypeName = "varbinary(max)")]
    public byte[]? SaltKey { get; set; }
}