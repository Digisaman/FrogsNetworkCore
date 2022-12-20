namespace FrogsNetwork.Freelancing.ViewModels
{
    public class UserProfileDisplyViewModel : UserProfileEditViewModel
    {
        public List<LookupItem> Countries { get; set; }

        public List<LookupItem> Regions { get; set; }

        public List<LookupItem> Cities { get; set; }
    }

}
