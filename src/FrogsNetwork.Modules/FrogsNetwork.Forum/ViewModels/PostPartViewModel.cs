using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;

namespace FrogsNetwork.Forum.ViewModels;
public class PostPartViewModel
{
    public string PostContentItemId => ContentItem.ContentItemId;
    public string ForumContentItemId { get; set; }
    public IEnumerable<ContentItem> ContentItems { get; set; }
    public dynamic Pager { get; set; }

    [BindNever]
    public ContentItem ContentItem { get; set; }
}
