namespace WebApplication2.Entities;

public class SailboatReservation
{
    public int IdSailboat { get; set; }
    public int IdReservation{ get; set; }
    public virtual Reservation Reservation { get; set; }
    public virtual Sailboat Sailboat { get; set; }
}