using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;

namespace FrogsNetwork.Forums.ViewModels;
[Serializable]
public class ForumEditViewModel
{
    public dynamic ForumEditor { get; set; }
    public string ForumId { get; set; }

    public ContentItem Item { get; set; }

    public string ReturnUrl { get; set; }
}
