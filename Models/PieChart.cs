using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

[NotMapped]
public class PieChart {
    public List<int> series { get; set; }
    public List<string> labels { get; set; }
}