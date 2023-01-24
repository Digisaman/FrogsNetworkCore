namespace FrogsNetwork.Freelancing.Models
{
    public class FreelancerLanguage
    {
        public virtual int Id { get; set; }
        public virtual int FreelancerId { get; set; }

        public virtual int LanguageId { get; set; }

        public virtual int Level { get; set; }
    }
}
