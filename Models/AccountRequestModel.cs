using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

[NotMapped]
public class AccountRequestModel{
    public string Account { get; set; }

    public string Line_Of_Business { get; set; }
}