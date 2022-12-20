using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrogsNetwork.Freelancing.ViewModels
{
    public class FreelancerProfileViewModel : UserProfileEditViewModel
    {

        public FreelancerProfileViewModel()
        {
            Countries = new List<SelectListItem>().AsEnumerable();
            Regions = new List<SelectListItem>().AsEnumerable();
            Cities = new List<SelectListItem>().AsEnumerable();
            Nationalities = new List<SelectListItem>().AsEnumerable();

            FreelancerNationalities = new List<FreelancerNationalityViewModel>();


        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
      "{0:yyyy-MM-dd}",
       ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        public string Mobile { get; set; }

        public string Website { get; set; }

        public int? SelectedNationalityId { get; set; }

        public IEnumerable<SelectListItem> Nationalities { get; set; }
        public List<FreelancerNationalityViewModel> FreelancerNationalities { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<SelectListItem> Regions { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

    }

}
