using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

[NotMapped]
public class PieChart{
    public int y { get; set; }
    public string label { get; set; }
}