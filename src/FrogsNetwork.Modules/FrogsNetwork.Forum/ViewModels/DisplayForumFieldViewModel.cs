using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentManagement;
using FrogsNetwork.Forum.Fields;

namespace FrogsNetwork.Forum.ViewModels;
public class DisplayForumFieldViewModel
{
    public string ForumContentItemId => Field.ForumContentItemId;
    public string[] PostContentItemIds => Field.PostContentItemIds;
    public ForumField Field { get; set; }
    public ContentPart Part { get; set; }
    public ContentPartFieldDefinition PartFieldDefinition { get; set; }
}
