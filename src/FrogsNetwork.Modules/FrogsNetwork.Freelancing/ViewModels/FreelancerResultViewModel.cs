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

        public string[] ExpertiseFirstIds { get; set; }
        public string[] ExpertiseSecondIds { get; set; }

        public string[] ServicesFirstIds { get; set; }
        public string[] ServicesSecondIds { get; set; }

        public int DistanceValue { get; set; }

        public string DistanceText { get; set; }

        public int DurationValue { get; set; }

        public string DurationText { get; set; }

    }

}
