using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

public class Account{
    [Key]
    [Column(TypeName = "nvarchar(50)")]
    public string ID { get; set; } //auto generated in back-end

    [Display(Name = "Account")]
    [Column(TypeName = "nvarchar(50)")]
    public string Account_Name { get; set; }

    [Display(Name = "Line Of Business")]
    [Column(TypeName = "nvarchar(50)")]
    public string LoB { get; set; }
}