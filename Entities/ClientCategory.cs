namespace WebApplication2.Entities;

public class ClientCategory
{
    public int IdClientCategory { get; set; }
    public string Name { get; set; }
    public int DiscountPerc { get; set; }
    public ICollection<Client> Clients { get; set; } = new List<Client>();
}