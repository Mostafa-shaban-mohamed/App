using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

public class Language{

    [Key]
    [Column(TypeName = "nvarchar(50)")] //auto generated in back-end
    public string ID { get; set; }

    [Display(Name = "Language")]
    [Column(TypeName = "nvarchar(50)")]
    public string Language_Name { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string Level { get; set; }

}