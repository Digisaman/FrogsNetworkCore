using FrogsNetwork.Forum.Models;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Navigation;
using YesSql;

namespace FrogsNetwork.Forum.Services;
public interface IForumService 
{
    Task<ForumPart> Get(string id, VersionOptions versionOptions);
    Task<IEnumerable<ForumPart>> Get();
    Task<IEnumerable<ForumPart>> Get(VersionOptions versionOptions);
    void Delete(ForumPart forum);
    IList<ContentTypeDefinition> GetForumTypes();
}

public class ForumService : IForumService
{
    private readonly IContentManager _contentManager;
    private readonly ISession _session;
    private readonly PagerOptions _pagerOptions;
    private readonly IContentDefinitionManager _contentDefinitionManager;


    public ForumService(IContentManager contentManager,
        ISession session,
        PagerOptions pagerOptions,
        IContentDefinitionManager contentDefinitionManager )
    {
        _contentManager = contentManager;
        _session = session;
        _pagerOptions = pagerOptions;
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<IEnumerable<ForumPart>> Get()
    {
        return await Get(VersionOptions.Published);
    }

    public async Task<IEnumerable<ForumPart>> Get(VersionOptions versionOptions)
    {
        //return _session.Query<ForumPart, ForumPartRecord>(versionOptions)
        //    .WithQueryHints(new QueryHints().ExpandRecords<AutoroutePartRecord, TitlePartRecord, CommonPartRecord>())
        //    .OrderBy(o => o.Weight)
        //    .List()
        //    .ToList();
        var query = await _session.Query<ForumPart>()
            .ListAsync();

        return query;
    }

    public async Task<ForumPart> Get(string id, VersionOptions versionOptions)
    {
        //return _contentManager.Query<ForumPart, ForumPartRecord>(versionOptions)
        //    .WithQueryHints(new QueryHints().ExpandRecords<AutoroutePartRecord, TitlePartRecord, CommonPartRecord>())
        //    .Where(x => x.Id == id)
        //    .List()
        //    .SingleOrDefault();

        var query = await _session.Query<ForumPart>()
           .With<ContentItemIndex>( c=> c.ContentItemId == id)
           .FirstOrDefaultAsync( );

        return query;
    }

    public void Delete(ForumPart forum)
    {
        //_contentManager.Remove(forum.ContentItem);
        _session.Delete(forum);
    }

    public IList<ContentTypeDefinition> GetForumTypes()
    {
        //var name = typeof(ForumPart).Name;

        //return _contentManager
        //    .GetContentTypeDefinitions()
        //    .Where(x =>
        //        x.Parts.Any(p => p.PartDefinition.Name == name))
        //    .Select(x => x)
        //    .ToList();

        var name = typeof(ForumPart).Name;

        return _contentDefinitionManager.ListTypeDefinitions()
            .Where( x =>
            x.Parts.Any(p => p.PartDefinition.Name == name))
            .Select( x => x)
            .ToList();
            
    }

   
}
