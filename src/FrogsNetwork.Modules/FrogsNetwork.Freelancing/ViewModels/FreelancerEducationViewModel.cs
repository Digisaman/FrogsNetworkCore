namespace FrogsNetwork.Freelancing.ViewModels
{
    public class FreelancerEducationViewModel
    {
        public int Id { get; set; }
        public int FreelancerId { get; set; }

        public int CountryId { get; set; }

        public string School { get; set; }

        public string CountryName { get; set; }

        public string City { get; set; }

        public string Degree { get; set; }

        public string Field { get; set; }

        public int EndYear { get; set; }
    }
}
