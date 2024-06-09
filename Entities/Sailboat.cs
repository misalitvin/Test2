using System.Data.SqlTypes;

namespace WebApplication2.Entities;

public class Sailboat
{
    public int IdSailboat { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public int IdBoatStandard { get; set; }
    public float Price { get; set; }
    public virtual BoatStandard BoatStandard { get; set; }
    
    public ICollection<SailboatReservation> SailboatReservations { get; set; } = new List<SailboatReservation>();
}