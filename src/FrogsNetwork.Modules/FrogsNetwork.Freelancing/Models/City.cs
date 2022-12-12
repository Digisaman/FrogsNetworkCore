namespace FrogsNetwork.Freelancing.Models;

public class City
{
    public virtual int Id { get; set; }

    public virtual string Name { get; set; } = "";

    public virtual int RegionId { get; set; }

}
