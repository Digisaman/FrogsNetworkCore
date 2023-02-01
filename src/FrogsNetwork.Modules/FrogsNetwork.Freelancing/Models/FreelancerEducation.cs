namespace FrogsNetwork.Freelancing.Models
{
    public class FreelancerEducation
    {
        public virtual int Id { get; set; }
        public virtual int FreelancerId { get; set; }

        public virtual int CountryId { get; set; }

        public virtual string City { get; set; }

        public virtual string Degree { get; set; }

        public virtual string Field { get; set; }

        public virtual int EndYear { get; set; }

        public virtual string School { get; set; }
    }
}
