using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

[NotMapped]
public class LanguageRequestModel
{
    public string Language { get; set; }

    public string Level { get; set; }
}