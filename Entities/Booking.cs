using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLotus.Main.Service.Entities;

public class Booking:IEntity
{
    public int BookingId { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int StaffId { get; set; }
    public Staff Staff { get; set; }    

    [NotMapped] 
    public int Id { get => BookingId; }
}