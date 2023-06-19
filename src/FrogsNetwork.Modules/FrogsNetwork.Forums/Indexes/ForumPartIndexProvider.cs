using FrogsNetwork.Forums.Models;
using OrchardCore.ContentManagement;
using System;
using YesSql.Indexes;

namespace FrogsNetwork.Forums.Indexes
{


    public class ForumPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context) =>
            context.For<ForumPartIndex>().Map(contentItem =>
            {
                var forumPart = contentItem.As<ForumPart>();


                return forumPart == null
                    ? null
                    : new ForumPartIndex
                    {
                        ContentItemId = contentItem.ContentItemId
                    };
            });
    }
}
