using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLotus.Main.Service.Entities;

public class Position 
{
    public int PositionId { get; set; }
    public string Name { get; set; }

    public List<Staff> Staffs { get; set; }

}