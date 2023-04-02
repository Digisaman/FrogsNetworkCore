using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;

namespace FrogsNetwork.Forum.Drivers;
public class PostFieldDriverHelper
{
    //public static void PopulateTermEntries(List<TermEntry> termEntries, TaxonomyField field, IEnumerable<ContentItem> contentItems, int level)
    //{
    //    foreach (var contentItem in contentItems)
    //    {
    //        var children = Array.Empty<ContentItem>();

    //        if (contentItem.Content.Terms is JArray termsArray)
    //        {
    //            children = termsArray.ToObject<ContentItem[]>();
    //        }

    //        var termEntry = new TermEntry
    //        {
    //            Term = contentItem,
    //            ContentItemId = contentItem.ContentItemId,
    //            Selected = field.TermContentItemIds.Contains(contentItem.ContentItemId),
    //            Level = level,
    //            IsLeaf = children.Length == 0
    //        };

    //        termEntries.Add(termEntry);

    //        if (children.Length > 0)
    //        {
    //            PopulateTermEntries(termEntries, field, children, level + 1);
    //        }
    //    }
    //}
}
