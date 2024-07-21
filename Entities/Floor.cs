using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLotus.Main.Service.Entities;

public class Floor:IEntity
{
    public int FloorId { get; set; }
    public string Name { get; set; }

    public List<Room> Rooms { get; set; }

    [NotMapped] 
    public int Id { get => FloorId; }
}