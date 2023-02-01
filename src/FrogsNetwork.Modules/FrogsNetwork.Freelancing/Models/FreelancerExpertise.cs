namespace FrogsNetwork.Freelancing.Models
{
    public class FreelancerExpertise
    {
        public virtual int Id { get; set; }

        public virtual int FreelancerId { get; set; }

        public virtual string ExpertiseId { get; set; }

        public virtual int LevelId { get; set; }
    }
}
