using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

public class User{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [DataType(DataType.Text)]
    public string Name { get; set; }

    [Required(ErrorMessage ="National ID is a required field")]
    [Column(TypeName = "nvarchar(14)")]
    [Display(Name = "National ID")]
    [StringLength(14, ErrorMessage ="National ID must be 14 digits")]
    [RegularExpression("(^[0-9]*)(^[12].*)", ErrorMessage = "National ID must be numeric")]
    public string National_ID { get; set; }

    [Display(Name = "Date of Birth")]
    public DateTime DoB { get; set; }

    [Required(ErrorMessage ="Age must be older than 18")]
    public int Age { get; set; }

    [Display(Name = "Account")]
    [Column(TypeName = "nvarchar(50)")]
    public string Account { get; set; } 

    [Display(Name = "Line of Business")]
    [Column(TypeName = "nvarchar(50)")]
    public string LoB { get; set; } 

    [Display(Name = "Language")]
    [Column(TypeName = "nvarchar(50)")]
    public string Language { get; set; }

    [Display(Name = "Level")]
    [Column(TypeName = "nvarchar(50)")]
    public string Level { get; set; } 
}