using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrogsNetwork.Freelancing.ViewModels
{
    public class FreelancerExpertiseViewModel
    {
        public FreelancerExpertiseViewModel()
        {
            this.ExpertiseFirst = new List<SelectListItem>().AsEnumerable();
            this.ExpertiseSecond = new List<SelectListItem>().AsEnumerable();

            this.ServicesFirst = new List<SelectListItem>().AsEnumerable();
            this.ServicesSecond = new List<SelectListItem>().AsEnumerable();

            this.Languages = new List<SelectListItem>().AsEnumerable();
            this.LanguageLevels = new List<SelectListItem>().AsEnumerable();

            this.FreelancerLanguages = new List<FreelancerLanguageViewModel>().AsEnumerable();
            this.FreelancerCertificates = new List<FreelancerCertificateViewModel>().AsEnumerable();
            this.FreelancerEducations = new List<FreelancerEducationViewModel>().AsEnumerable();
            this.Countries = new List<SelectListItem>().AsEnumerable();
        }

        public int Id { get; set; }


        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<SelectListItem> ExpertiseFirst { get; set; }
        public IEnumerable<SelectListItem> ExpertiseSecond { get; set; }

        public IEnumerable<SelectListItem> ServicesFirst { get; set; }
        public IEnumerable<SelectListItem> ServicesSecond { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }

        public IEnumerable<SelectListItem> LanguageLevels { get; set; }
        public IEnumerable<FreelancerLanguageViewModel> FreelancerLanguages { get; set; }
        public IEnumerable<FreelancerCertificateViewModel> FreelancerCertificates { get; set; }

        public IEnumerable<FreelancerEducationViewModel> FreelancerEducations { get; set; }

        public string[] ExpertiseFirstIds { get; set; }
        public string[] ExpertiseSecondIds { get; set; }

        public string[] ServicesFirstIds { get; set; }
        public string[] ServicesSecondIds { get; set; }

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
