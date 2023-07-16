namespace BasedBaunApi.Models;

public class ItemDto
{
    public class ItemCreateDto
    {
        public string Description { get; set; }
        public Item.Lanes Lane { get; set; }
    }
 
    public class ItemReadDto
    {
        public int ItemId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public Item.Lanes  Lane { get; set; }
    }

    public class ItemUpdateDto : ItemCreateDto
    {
        public int ItemId { get; set; }
    }
}