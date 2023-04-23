using FrogsNetwork.Forum.Models;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Navigation;
using OrchardCore.Users;
using YesSql;

namespace FrogsNetwork.Forum.Services;

public interface IThreadService 
{
    Task<ThreadPart> Get(long forumId, long threadId, VersionOptions versionOptions);
    Task<ThreadPart> Get(string id, VersionOptions versionOptions);
    Task<IEnumerable<ThreadPart>> Get(ForumPart forumPart);
    Task<IEnumerable<ThreadPart>> Get(ForumPart forumPart, VersionOptions versionOptions);
    Task<IEnumerable<ThreadPart>> Get(ForumPart forumPart, int skip, int count);
    Task<IEnumerable<ThreadPart>> Get(ForumPart forumPart, int skip, int count, VersionOptions versionOptions);
    Task<int> Count(ForumPart forumPart, VersionOptions versionOptions);
    Task Delete(ForumPart forumPart);
}

public class ThreadService : IThreadService
{
    private readonly IContentManager _contentManager;
    private readonly ISession _session;
    private readonly PagerOptions _pagerOptions;


    public ThreadService(IContentManager contentManager)
    {
        _contentManager = contentManager;
    }

    public async Task<ThreadPart> Get(long forumId, long threadId, VersionOptions versionOptions)
    {
        //return _contentManager.Query<CommonPart, CommonPartRecord>(versionOptions)
        //          .Where(cpr => cpr.Container.Id == forumId)
        //          .Join<ThreadPartRecord>()
        //          .Where(o => o.Id == threadId)
        //          .WithQueryHints(new QueryHints().ExpandRecords<AutoroutePartRecord, TitlePartRecord, CommonPartRecord>())
        //          .ForPart<ThreadPart>()
        //          .Slice(1)
        //          .SingleOrDefault();var query = await _session.Query<ThreadPart>()
        var query = await _session.Query<ThreadPart>()
         .With<ContentItemIndex>(c => c.Id == threadId)
         .FirstOrDefaultAsync();
        return query;
    }

    public async Task<ThreadPart> Get(string id, VersionOptions versionOptions)
    {
        //return _contentManager
        //    .Query<ThreadPart, ThreadPartRecord>(versionOptions)
        //    .WithQueryHints(new QueryHints().ExpandRecords<AutoroutePartRecord, TitlePartRecord, CommonPartRecord>())
        //    .Where(x => x.Id == id).Slice(1).SingleOrDefault();

        var query = await _session.Query<ThreadPart>()
         .With<ContentItemIndex>(c => c.ContentItemId == id)
         .FirstOrDefaultAsync();
        return query;
    }

    public async Task<IEnumerable<ThreadPart>> Get(ForumPart forumPart)
    {
        return await Get(forumPart, VersionOptions.Published);
    }

    public async Task<IEnumerable<ThreadPart>> Get(ForumPart forumPart, VersionOptions versionOptions)
    {
        return await Get(forumPart, 0, 0, versionOptions);
    }

    public async Task<IEnumerable<ThreadPart>> Get(ForumPart forumPart, int skip, int count)
    {
        return await Get(forumPart, skip, count, VersionOptions.Published);
    }

    public async Task<IEnumerable<ThreadPart>> Get(ForumPart forumPart, int skip, int count, VersionOptions versionOptions)
    {
        //return GetParentQuery(forumPart, versionOptions)
        //    .Join<ThreadPartRecord>()
        //    .OrderByDescending(o => o.IsSticky)
        //    .Join<CommonPartRecord>()
        //    .OrderByDescending(o => o.ModifiedUtc)
        //    .ForPart<ThreadPart>()
        //    .Slice(skip, count)
        //    .ToList();

        var query = await _session.Query<ThreadPart>()
            .Take(count)
            .Skip(skip) 
            .ListAsync();
        return query;
    }

    public async Task<IEnumerable<ThreadPart>> Get(ForumPart forumPart, IUser user)
    {
        var query = await _session.Query<ThreadPart>()
            .With<ContentItemIndex>(c => c.Author == user.UserName)
            .ListAsync();
        return query;
    }

    public async Task<int> Count(ForumPart forumPart, VersionOptions versionOptions)
    {
        var query = await _session.Query<ThreadPart>()
            .CountAsync();
        return query;
    }

    public async Task Delete(ForumPart forumPart)
    {
        IEnumerable<ThreadPart> threads = await Get(forumPart);
        foreach (var thread in  threads)
        {
            _contentManager.RemoveAsync(thread.ContentItem);
        }
        return; 
    }

   

    //private Query<CommonPart> GetParentQuery(IContent parentPart, VersionOptions versionOptions)
    //{
    //    return _session.Query<CommonPart>

    //}
}
