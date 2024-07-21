using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLotus.Main.Service.Entities;

public class Staff:IEntity
{
    public int StaffId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public int PositionId { get; set; }
    public Position Position { get; set; }

    public List<Booking> Bookings { get; set; }

    [NotMapped]
    public int Id { get => StaffId; }
}