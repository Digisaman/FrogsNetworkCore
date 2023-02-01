namespace FrogsNetwork.Freelancing.Models
{
    public class FreelancerCertificate
    {
        public virtual int Id { get; set; }
        public virtual int FreelancerId { get; set; }

        public virtual string Certificate { get; set; }

        public virtual string Organization { get; set; }

        public virtual string Description { get; set; }
    }
}
