using System.Data.SqlTypes;
using System.Text.Json.Serialization;

namespace WebApplication2.Entities;

public class Reservation
{
    public int IdReservation { get; set; }
    public int IdClient { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int IdBoatStandard { get; set; }
    public int Capacity { get; set; }
    public int NumOfBoats { get; set; }
    public bool Fullfilled { get; set; }
    public float? Price { get; set; }
    public string? CancelReason { get; set; }
    public virtual Client Client { get; set; }
    public virtual BoatStandard BoatStandard { get; set; }
    [JsonIgnore]
    public ICollection<SailboatReservation> SailboatReservations { get; set; } = new List<SailboatReservation>();
}