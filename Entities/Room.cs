using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLotus.Main.Service.Entities;

public class Room
{
    public int RoomId { get; set; }
    public int Number { get; set; }
    
   public int FloorId { get; set; }
   public Floor Floor { get; set; }

   public int RoomTypeId { get; set; }
   public RoomType RoomType { get; set; }

   public int StatusId { get; set; }
   public Status Status { get; set; }

}