using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement.Routing;
using FrogsNetwork.Forum.Models;

namespace FrogsNetwork.Forum.Handlers;
public class ForumPartHandler : ContentPartHandler<ForumPart>
{
    public override Task GetContentItemAspectAsync(ContentItemAspectContext context, ForumPart part)
    {
        return context.ForAsync<ContainedContentItemsAspect>(aspect =>
        {
            aspect.Accessors.Add((jObject) =>
            {
                return jObject["ForumPart"]["Posts"] as JArray;
            });

            return Task.CompletedTask;
        });
    }
}
