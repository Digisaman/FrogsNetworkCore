using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data;
using FrogsNetwork.Forum.Fields;
using YesSql.Indexes;

namespace FrogsNetwork.Forum.Indexing;

public class ForumIndexProvider : IndexProvider<ContentItem>, IScopedIndexProvider
{
    private readonly IServiceProvider _serviceProvider;
    private readonly HashSet<string> _ignoredTypes = new HashSet<string>();
    private IContentDefinitionManager _contentDefinitionManager;

    public ForumIndexProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override void Describe(DescribeContext<ContentItem> context)
    {
        context.For<ForumIndex>()
            .Map(contentItem =>
            {
                // Remove index records of soft deleted items.
                if (!contentItem.Published && !contentItem.Latest)
                {
                    return null;
                }

                // Can we safely ignore this content item?
                if (_ignoredTypes.Contains(contentItem.ContentType))
                {
                    return null;
                }

                // Lazy initialization because of ISession cyclic dependency
                _contentDefinitionManager ??= _serviceProvider.GetRequiredService<IContentDefinitionManager>();

                // Search for Forum fields
                var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(contentItem.ContentType);

                // This can occur when content items become orphaned, particularly layer widgets when a layer is removed, before its widgets have been unpublished.
                if (contentTypeDefinition == null)
                {
                    _ignoredTypes.Add(contentItem.ContentType);
                    return null;
                }

                var fieldDefinitions = contentTypeDefinition
                    .Parts.SelectMany(x => x.PartDefinition.Fields.Where(f => f.FieldDefinition.Name == nameof(ForumField)))
                    .ToArray();

                // This type doesn't have any ForumField, ignore it
                if (fieldDefinitions.Length == 0)
                {
                    _ignoredTypes.Add(contentItem.ContentType);
                    return null;
                }

                var results = new List<ForumIndex>();

                // Get all field values
                foreach (var fieldDefinition in fieldDefinitions)
                {
                    var jPart = (JObject)contentItem.Content[fieldDefinition.PartDefinition.Name];

                    if (jPart == null)
                    {
                        continue;
                    }

                    var jField = jPart[fieldDefinition.Name] as JObject;

                    if (jField == null)
                    {
                        continue;
                    }

                    var field = jField.ToObject<ForumField>();

                    foreach (var PostContentItemId in field.PostContentItemIds)
                    {
                        results.Add(new ForumIndex
                        {
                            ForumContentItemId = field.ForumContentItemId,
                            ContentItemId = contentItem.ContentItemId,
                            ContentType = contentItem.ContentType,
                            ContentPart = fieldDefinition.PartDefinition.Name,
                            ContentField = fieldDefinition.Name,
                            PostContentItemId = PostContentItemId,
                            Published = contentItem.Published,
                            Latest = contentItem.Latest
                        });
                    }
                }

                return results;
            });
    }
}
