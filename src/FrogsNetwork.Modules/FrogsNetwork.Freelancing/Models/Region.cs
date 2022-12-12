namespace FrogsNetwork.Freelancing.Models;

public class Region
{
    public virtual int Id { get; set; }

    public virtual string Name { get; set; } = "";

    public virtual int CountryId { get; set; }
}
