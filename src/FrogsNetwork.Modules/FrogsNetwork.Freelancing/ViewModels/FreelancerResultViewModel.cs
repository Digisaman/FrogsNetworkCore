namespace FrogsNetwork.Freelancing.ViewModels
{
    public class FreelancerResultViewModel : UserProfileEditViewModel
    {

        public FreelancerResultViewModel()
        {

        }

        public string City { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public int[] ExpertiseFirstIds { get; set; }
        public int[] ExpertiseSecondIds { get; set; }

        public int[] ServicesFirstIds { get; set; }
        public int[] ServicesSecondIds { get; set; }

    }

}
