namespace WebApplication2.DTOs;

public class ReservationDto
{
    public int IdClient { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int NumOfBoats { get; set; }
    public int IdBoatStandard { get; set; }
}