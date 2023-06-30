using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

[NotMapped]
public class LineChart{
    public List<int> x { get; set; }

    public List<int> y { get; set; }
}