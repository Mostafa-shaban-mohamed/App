using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

[NotMapped]
public class UserViewModel{

    public string Name { get; set; }

    [Required(ErrorMessage ="National ID is a required field")]
    [Display(Name = "National ID")]
    [MaxLength(14)]
    [MinLength(14)]
    [RegularExpression("(^[0-9]*)(^[12].*)", ErrorMessage = "National ID must be numeric")]
    public string National_ID { get; set; }

    [Display(Name = "Date of Birth")]
    public string DoB { get; set; }

    [Display(Name = "Account")]
    public string Account { get; set; } 

    [Display(Name = "Line of Business")]
    public string LoB { get; set; } 

    [Display(Name = "Language")]
    public string Language { get; set; }

    [Display(Name = "Language Level")]
    public string Level { get; set; } 
}