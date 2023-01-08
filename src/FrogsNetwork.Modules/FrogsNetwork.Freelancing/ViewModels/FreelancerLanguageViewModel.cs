namespace FrogsNetwork.Freelancing.ViewModels
{
    public class FreelancerLanguageViewModel
    {
        public int FreelancerId { get; set; }

        public int Id { get; set; }

        public int LanguageId { get; set; }

        public string LanguageTitle { get; set; }

        public int LevelId { get; set; }

        public string LevelTitle { get; set; }
    }


    public class FreelancerCertificateViewModel
    {
        public int Id { get; set; }
        public string Certificate { get; set; }

        public string Organization { get; set; }

        public string Description { get; set; }
    }
}
