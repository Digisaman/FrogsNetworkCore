using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Forum.Fields;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Models;

namespace FrogsNetwork.Forum.ViewModels;
public class EditForumFieldViewModel
{
    public string ForumContentItemId { get; set; }
    public string[] PostContentItemIds { get; set; }
    [BindNever]
    public ContentItem Forum { get; set; }

    [BindNever]
    public ForumField Field { get; set; }

    [BindNever]
    public ContentPart Part { get; set; }

    [BindNever]
    public ContentPartFieldDefinition PartFieldDefinition { get; set; }
}
