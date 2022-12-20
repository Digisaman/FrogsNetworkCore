using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrogsNetwork.Freelancing.ViewModels
{
    public class CompanyProfileViewModel : UserProfileEditViewModel
    {   
        public string Website { get; set; }

        public string ContactPersonPosition { get; set; }

        public string ContactPersonName { get; set; }

        public string CompanyName { get; set; }

        public string Activities { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public List<SelectListItem> Regions { get; set; }

        public List<SelectListItem> Cities { get; set; }

        public CompanyProfileViewModel()
        {
            Countries = new List<SelectListItem>();
            Regions = new List<SelectListItem>();
            Cities = new List<SelectListItem>();
        }
    }

}
