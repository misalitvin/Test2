namespace WebApplication2.Entities;

public class BoatStandard
{
    public int IdBoatStandard { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Sailboat> Sailboats { get; set; } = new List<Sailboat>();

}