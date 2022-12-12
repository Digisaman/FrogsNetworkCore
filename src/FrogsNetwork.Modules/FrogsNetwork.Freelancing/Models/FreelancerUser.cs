namespace FrogsNetwork.Freelancing.Models;

public class FreelancerUser : BaseUser
{
    public virtual int Id { get; set; }

    public virtual int UserId { get; set; }

    public virtual string Tel { get; set; } = "";

    public virtual DateTime? BirthDate { get; set; }

    public virtual string Mobile { get; set; } = "";

    public virtual string FirstName { get; set; } = ""; 

    public virtual string LastName { get; set; } = "";

    public virtual string Website { get; set; } = ""; 
}
