using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.Contents.Indexing;
using OrchardCore.Indexing;
using FrogsNetwork.Forum.Fields;

namespace FrogsNetwork.Forum.Indexing;
public class ForumFieldIndexHandler : ContentFieldIndexHandler<ForumField>
{
    private readonly IServiceProvider _serviceProvider;

    public ForumFieldIndexHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override async Task BuildIndexAsync(ForumField field, BuildFieldIndexContext context)
    {
        // TODO: Also add the parents of each Post, probably as a separate field

        var options = context.Settings.ToOptions();
        options |= DocumentIndexOptions.Keyword | DocumentIndexOptions.Store;

        // Directly selected Post ids are added to the default field name
        foreach (var contentItemId in field.PostContentItemIds)
        {
            foreach (var key in context.Keys)
            {
                context.DocumentIndex.Set(key + IndexingConstants.IdsKey, contentItemId, options);
            }
        }

        // Inherited Post ids are added to a distinct field, prefixed with "Inherited"
        var contentManager = _serviceProvider.GetRequiredService<IContentManager>();
        var Forum = await contentManager.GetAsync(field.ForumContentItemId);

        var inheritedContentItems = new List<ContentItem>();

        //foreach (var contentItemId in field.PostContentItemIds)
        //{
        //    ForumOrchardHelperExtensions.FindPostHierarchy(Forum.Content.ForumPart.Posts as JArray, contentItemId, inheritedContentItems);
        //}

        foreach (var key in context.Keys)
        {
            foreach (var contentItem in inheritedContentItems)
            {
                context.DocumentIndex.Set(key + IndexingConstants.InheritedKey, contentItem.ContentItemId, options);
            }
        }
    }
}

