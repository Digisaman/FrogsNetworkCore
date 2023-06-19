using OrchardCore.ContentManagement;
using YesSql.Indexes;

namespace FrogsNetwork.Forums.Indexes
{
    public class ForumPartIndex : MapIndex
    {
        public string ContentItemId { get; set; }
        //public string[] PostIdss { get; set; }

        //public DateTime? LastModified { get; set; }
    }
}
