namespace FrogsNetwork.Freelancing.Models
{
    public class FreelancerService
    {
        public virtual int Id { get; set; }

        public virtual int FreelancerId { get; set; }

        public virtual string ServiceId { get; set; }

        public virtual int LevelId { get; set; }
    }
}
