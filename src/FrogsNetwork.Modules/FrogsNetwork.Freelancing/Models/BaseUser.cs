namespace FrogsNetwork.Freelancing.Models;

public class BaseUser
{
    public virtual int CountryId { get; set; }

    public virtual int RegionId { get; set; }

    public virtual int CityId { get; set; }

    public virtual string Lat { get; set; } = "";

    public virtual string Long { get; set; } = "";
    public virtual string VAT { get; set; } = "";   
    public virtual string Address { get; set; } = "";   
    public virtual string PostalCode { get; set; } = "";    
}
