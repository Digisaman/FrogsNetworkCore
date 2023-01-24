namespace FrogsNetwork.Freelancing.Models
{
    public class FreelancerNationality
    {
        public virtual int Id { get; set; }
        public virtual int FreelancerId { get; set; }

        public virtual int NationalityId { get; set; }
    }
}
