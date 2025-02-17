using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLotus.Main.Service.Entities;

public class RoomType
{
    public int RoomTypeId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public List<Room> Rooms { get; set; }

}