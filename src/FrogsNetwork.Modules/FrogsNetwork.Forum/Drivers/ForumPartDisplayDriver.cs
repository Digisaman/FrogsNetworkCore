using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using FrogsNetwork.Forum.Models;
using FrogsNetwork.Forum.ViewModels;

namespace FrogsNetwork.Forum.Drivers;
public class ForumPartDisplayDriver : ContentPartDisplayDriver<ForumPart>
{
    public override IDisplayResult Display(ForumPart part, BuildPartDisplayContext context)
    {
        var hasItems = part.Posts.Any();
        return Initialize<ForumPartViewModel>(hasItems ? "ForumPart" : "ForumPart_Empty", m =>
        {
            m.ContentItem = part.ContentItem;
            m.ForumPart = part;
        })
        .Location("Detail", "Content:5");
    }

    public override IDisplayResult Edit(ForumPart part)
    {
        return Initialize<ForumPartEditViewModel>("ForumPart_Edit", model =>
        {
            model.PostContentType = part.PostContentType;
            model.ForumPart = part;
        });
    }

    public override async Task<IDisplayResult> UpdateAsync(ForumPart part, IUpdateModel updater)
    {
        var model = new ForumPartEditViewModel();

        if (await updater.TryUpdateModelAsync(model, Prefix, t => t.Hierarchy, t => t.PostContentType))
        {
            if (!String.IsNullOrWhiteSpace(model.Hierarchy))
            {
                var originalForumItems = part.ContentItem.As<ForumPart>();

                var newHierarchy = JArray.Parse(model.Hierarchy);

                var ForumItems = new JArray();

                foreach (var item in newHierarchy)
                {
                    ForumItems.Add(ProcessItem(originalForumItems, item as JObject));
                }

                part.Posts = ForumItems.ToObject<List<ContentItem>>();
            }

            part.PostContentType = model.PostContentType;
        }

        return Edit(part);
    }

    /// <summary>
    /// Clone the content items at the specific index.
    /// </summary>
    private JObject GetForumItemAt(List<ContentItem> forumItems, int[] indexes)
    {
        ContentItem forumItem = null;

        // Seek the term represented by the list of indexes
        foreach (var index in indexes)
        {
            if (forumItems == null || forumItems.Count < index)
            {
                // Trying to acces an unknown index
                return null;
            }

            forumItem = forumItems[index];

            var terms = forumItem.Content.Terms as JArray;
            forumItems = terms?.ToObject<List<ContentItem>>();
        }

        var newObj = JObject.Parse(JsonConvert.SerializeObject(forumItem));

        if (newObj["Posts"] != null)
        {
            newObj["Posts"] = new JArray();
        }

        return newObj;
    }

    private JObject ProcessItem(ForumPart originalItems, JObject item)
    {
        var contentItem = GetForumItemAt(originalItems.Posts, item["index"].ToString().Split('-').Select(x => Convert.ToInt32(x)).ToArray());

        var children = item["children"] as JArray;

        if (children != null)
        {
            var forumItems = new JArray();

            for (var i = 0; i < children.Count; i++)
            {
                forumItems.Add(ProcessItem(originalItems, children[i] as JObject));
                contentItem["Posts"] = forumItems;
            }
        }

        return contentItem;
    }
}
