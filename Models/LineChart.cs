using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

[NotMapped]
public class LineChart{
    public int x { get; set; }

    public int y { get; set; }
}