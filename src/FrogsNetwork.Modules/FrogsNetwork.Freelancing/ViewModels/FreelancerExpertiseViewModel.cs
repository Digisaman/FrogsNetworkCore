using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrogsNetwork.Freelancing.ViewModels
{
    public class FreelancerExpertiseViewModel
    {
        public FreelancerExpertiseViewModel()
        {
            this.ExpertiseFirst = new List<SelectListItem>();
            this.ExpertiseSecond = new List<SelectListItem>();

            this.ServicesFirst = new List<SelectListItem>();
            this.ServicesSecond = new List<SelectListItem>();

            this.Languages = new List<SelectListItem>();
            this.LanguageLevels = new List<SelectListItem>();

            this.FreelancerLanguages = new List<FreelancerLanguageViewModel>();
            this.FreelancerCertificates = new List<FreelancerCertificateViewModel>();
            this.FreelancerEducations = new List<FreelancerEducationViewModel>();
            this.Countries = new List<SelectListItem>();
        }

        public int Id { get; set; }


        public List<SelectListItem> Countries { get; set; }

        public List<SelectListItem> ExpertiseFirst { get; set; }
        public List<SelectListItem> ExpertiseSecond { get; set; }

        public List<SelectListItem> ServicesFirst { get; set; }
        public List<SelectListItem> ServicesSecond { get; set; }

        public List<SelectListItem> Languages { get; set; }

        public List<SelectListItem> LanguageLevels { get; set; }
        public List<FreelancerLanguageViewModel> FreelancerLanguages { get; set; }
        public List<FreelancerCertificateViewModel> FreelancerCertificates { get; set; }

        public List<FreelancerEducationViewModel> FreelancerEducations { get; set; }

        public int[] ExpertiseFirstIds { get; set; }
        public int[] ExpertiseSecondIds { get; set; }

        public int[] ServicesFirstIds { get; set; }
        public int[] ServicesSecondIds { get; set; }

        public int SelectedLanguageId { get; set; }

        public int SelectedLevelId { get; set; }

        public string CertificateTitle { get; set; }

        public string CertificateOrganization { get; set; }

        public string CertificateDesctiption { get; set; }

        public string EducationSchool { get; set; }

        public string EducationDegree { get; set; }

        public string EducationField { get; set; }

        public string EducationCity { get; set; }

        public int EducationEndYear { get; set; }
        public int EducationCountryId { get; set; }
    }




}
