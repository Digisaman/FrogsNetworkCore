using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;

namespace FrogsNetwork.Forum.Fields;
public class ForumField : ContentField
{
    public string ForumContentItemId { get; set; } = "";
    public string[] PostContentItemIds { get; set; } = Array.Empty<string>();

}
