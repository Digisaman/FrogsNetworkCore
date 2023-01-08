using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrogsNetwork.Freelancing.ViewModels
{
    public class FreelancerSearchViewModel
    {

        public FreelancerSearchViewModel()
        {
            Countries = new List<SelectListItem>().AsEnumerable();
            Regions = new List<SelectListItem>().AsEnumerable();
            Cities = new List<SelectListItem>().AsEnumerable();

            ExpertiseFirst = new List<SelectListItem>();
            ExpertiseSecond = new List<SelectListItem>();

            ServicesFirst = new List<SelectListItem>();
            ServicesSecond = new List<SelectListItem>();

            Freelancers = new List<FreelancerResultViewModel>();

            SelectedFreelancer = new FreelancerResultViewModel();

        }

        public bool AnyFilterSelected()
        {
            if (CountryId != 0 || RegionId != 0 || CityId != 0)
                return true;
            if (ExpertiseFirstIds != null && ExpertiseFirstIds.Any() || ExpertiseSecondIds != null && ExpertiseSecondIds.Any())
                return true;

            if (ServicesFirstIds != null && ServicesFirstIds.Any() || ServicesSecondIds != null && ServicesSecondIds.Any())
                return true;

            return false;
        }

        public string Lat { get; set; }

        public string Long { get; set; }

        public int CountryId { get; set; }

        public int RegionId { get; set; }

        public int CityId { get; set; }

        public int[] ExpertiseFirstIds { get; set; }
        public int[] ExpertiseSecondIds { get; set; }
        public int[] ServicesFirstIds { get; set; }
        public int[] ServicesSecondIds { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<SelectListItem> Regions { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        public List<SelectListItem> ExpertiseFirst { get; set; }
        public List<SelectListItem> ExpertiseSecond { get; set; }

        public List<SelectListItem> ServicesFirst { get; set; }
        public List<SelectListItem> ServicesSecond { get; set; }

        public List<FreelancerResultViewModel> Freelancers { get; set; }

        public FreelancerResultViewModel SelectedFreelancer { get; set; }
        public string Address { get; set; }
    }

}
