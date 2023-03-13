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

            ExpertiseFirst = new List<SelectListItem>().AsEnumerable();
            ExpertiseSecond = new List<SelectListItem>().AsEnumerable();

            ServicesFirst = new List<SelectListItem>().AsEnumerable();
            ServicesSecond = new List<SelectListItem>().AsEnumerable();

            Freelancers = new List<FreelancerResultViewModel>().AsEnumerable();

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

        public int? CountryId { get; set; }

        public int? RegionId { get; set; }

        public int? CityId { get; set; }

        public string[] ExpertiseFirstIds { get; set; }
        public string[] ExpertiseSecondIds { get; set; }
        public string[] ServicesFirstIds { get; set; }
        public string[] ServicesSecondIds { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<SelectListItem> Regions { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        public IEnumerable<SelectListItem> ExpertiseFirst { get; set; }
        public IEnumerable<SelectListItem> ExpertiseSecond { get; set; }

        public IEnumerable<SelectListItem> ServicesFirst { get; set; }
        public IEnumerable<SelectListItem> ServicesSecond { get; set; }

        public IEnumerable<FreelancerResultViewModel> Freelancers { get; set; }

        public FreelancerResultViewModel SelectedFreelancer { get; set; }
        public string Address { get; set; }

        //public string PostalCode { get; set; }
    }

}
