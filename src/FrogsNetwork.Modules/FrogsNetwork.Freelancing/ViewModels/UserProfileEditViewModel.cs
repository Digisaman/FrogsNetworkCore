using Microsoft.AspNetCore.Mvc.Rendering;
namespace FrogsNetwork.Freelancing.ViewModels
{
    public class UserProfileEditViewModel
    {
        public UserProfileEditViewModel()
        {
            Countries = new List<SelectListItem>().AsEnumerable();
            Regions = new List<SelectListItem>().AsEnumerable();
            Cities = new List<SelectListItem>().AsEnumerable();
        }
        public int Id { get; set; }

        public string UserId { get; set; }

        public int CountryId { get; set; }

        public int RegionId { get; set; }

        public int CityId { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }

        public string VAT { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string Tel { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<SelectListItem> Regions { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        
    }

}
