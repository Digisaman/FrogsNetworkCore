using System;
using System.Collections.Generic;
using System.Linq;


namespace FrogsNetwork.Freelancing.ViewModels
{
    public class FreelancerDetailViewModel : UserProfileEditViewModel
    {

        public FreelancerDetailViewModel()
        {
            Expertise = new List<string>().AsEnumerable();
            Services = new List<string>().AsEnumerable();
            Nationalities = new List<FreelancerNationalityViewModel>().AsEnumerable();
            Languages = new List<FreelancerLanguageViewModel>().AsEnumerable();
            Certificates = new List<FreelancerCertificateViewModel>().AsEnumerable();
            Educations = new List<FreelancerEducationViewModel>().AsEnumerable();


        }

        public string Email { get; set; }
        public string CityTitle { get; set; }

        public string RegionTitle { get; set; }

        public string CountryTitle { get; set; }

        public IEnumerable<string> Expertise { get; set; }

        public IEnumerable<string> Services { get; set; }
        public IEnumerable<FreelancerNationalityViewModel> Nationalities { get; set; }
        public IEnumerable<FreelancerLanguageViewModel> Languages { get;  set; }
        public IEnumerable<FreelancerCertificateViewModel> Certificates { get;  set; }
        public IEnumerable<FreelancerEducationViewModel> Educations { get; set; }
        public string Mobile { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Website { get; set; }
        public string CompanyTel { get; set; }
    }

}
