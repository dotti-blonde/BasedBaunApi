using System.ComponentModel.DataAnnotations.Schema;

namespace BasedBaunApi.Models;

public class Item
{
    public enum Lanes
    {
        Todo,
        Doing,
        Done
    }

    public int ItemId { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
    public Lanes Lane { get; set; }
}