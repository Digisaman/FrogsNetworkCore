using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Forum.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;

namespace FrogsNetwork.Forum.ViewModels;
public class ForumPartViewModel
{
    public string ForumContentItemId => ContentItem.ContentItemId;

    [BindNever]
    public ContentItem ContentItem { get; set; }

    [BindNever]
    public ForumPart ForumPart { get; set; }
}

public class ForumPartEditViewModel
{
    public string Hierarchy { get; set; }

    public string PostContentType { get; set; }

    [BindNever]
    public ForumPart ForumPart { get; set; }
}
