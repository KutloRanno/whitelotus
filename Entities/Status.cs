using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLotus.Main.Service.Entities;

public class Status
{
    public int StatusId { get; set; }
    public string Name { get; set; }

    public List<Room> Rooms { get; set; }

}