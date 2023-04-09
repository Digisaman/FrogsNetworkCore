using FrogsNetwork.Forum.Indexing;
using FrogsNetwork.Forum.Models;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Navigation;
using YesSql;

namespace FrogsNetwork.Forum.Services;
public interface IPostService 
{
    //Task<PostPart> Get(int id, VersionOptions versionOptions);
    //Task<IEnumerable<PostPart>> Get(ThreadPart threadPart);
    //Task<IEnumerable<PostPart>> Get(ThreadPart threadPart, VersionOptions versionOptions);
    //Task<IEnumerable<PostPart>> Get(ThreadPart threadPart, int skip, int count);
    //Task<IEnumerable<PostPart>> Get(ThreadPart threadPart, int skip, int count, VersionOptions versionOptions);
    //Task<PostPart> GetPositional(ThreadPart threadPart,
    //                       ThreadPostPositional positional);
    //Task<PostPart> GetPositional(ThreadPart threadPart, VersionOptions versionOptions,
    //                       ThreadPostPositional positional);
    //Task<IEnumerable<IUser>> GetUsersPosted(ThreadPart part);
    //int Count(ThreadPart threadPart, VersionOptions versionOptions);
    //void Delete(ThreadPart threadPart);
    Task<ForumPart> GetParentForumQuery(PostPart postPart, VersionOptions versionOptions);
    Task<PostPart> GetParentPostQuery(ThreadPart threadPart, VersionOptions versionOptions);
}

public class PostService : IPostService
{

    private readonly IContentManager _contentManager;
    private readonly ISession _session;
    private readonly PagerOptions _pagerOptions;

    public PostService(IContentManager contentManager,
         ISession session,
        PagerOptions pagerOptions
        //, IRepository<CommonPartRecord> commonRepository
        )
    {
        _contentManager = contentManager;
        _session = session;
        _pagerOptions = pagerOptions;
        //_commonRepository = commonRepository;
    }

    //public Task<IEnumerable<PostPart>> Get(ThreadPart threadPart)
    //{
    //    return Get(threadPart, VersionOptions.Published);
    //}

    //public Task<IEnumerable<PostPart>> Get(ThreadPart threadPart, VersionOptions versionOptions)
    //{
    //    return GetParentPostQuery(threadPart, versionOptions)
    //        .ForPart<PostPart>()
    //        .List();
    //}

    public async Task<PostPart> Get(string id, VersionOptions versionOptions)
    {
        //return _contentManager.Query<PostPart, PostPartRecord>(versionOptions)
        //    .WithQueryHints(new QueryHints().ExpandRecords<CommonPartRecord>())
        //    .Where(x => x.Id == id)
        //    .List()
        //    .SingleOrDefault();

        var query = await _session.Query<PostPart>()
           .With<ContentItemIndex>(c => c.ContentItemId == id)
           .FirstOrDefaultAsync();

        return query;
    }

    //public PostPart GetPositional(ThreadPart threadPart,
    //    ThreadPostPositional positional)
    //{
    //    return GetPositional(threadPart, VersionOptions.Published, positional);
    //}

    //public PostPart GetPositional(ThreadPart threadPart, VersionOptions versionOptions,
    //                              ThreadPostPositional positional)
    //{
    //    var query = GetParentQuery(threadPart, versionOptions);

    //    if (positional == ThreadPostPositional.First)
    //        query = query.OrderBy(o => o.PublishedUtc);

    //    if (positional == ThreadPostPositional.Latest)
    //        query = query.OrderByDescending(o => o.PublishedUtc);

    //    return query
    //        .ForPart<PostPart>()
    //        .Slice(1)
    //        .SingleOrDefault();
    //}

    //public IEnumerable<IUser> GetUsersPosted(ThreadPart part)
    //{
    //    var users = _commonRepository.Table.Where(o => o.Container.Id == part.Id)
    //                     .Select(o => o.OwnerId)
    //                     .Distinct();

    //    return _contentManager
    //        .GetMany<IUser>(users, VersionOptions.Published, new QueryHints())
    //        .ToList();
    //}

    //public int Count(ThreadPart threadPart, VersionOptions versionOptions)
    //{
    //    return GetParentQuery(threadPart, versionOptions).Count();
    //}

    //public void Delete(ThreadPart threadPart)
    //{
    //    Get(threadPart, VersionOptions.AllVersions)
    //        .ToList()
    //        .ForEach(post => _contentManager.Remove(post.ContentItem));
    //}

    //public IEnumerable<PostPart> Get(ThreadPart threadPart, int skip, int count)
    //{
    //    return Get(threadPart, skip, count, VersionOptions.Published);
    //}

    //public IEnumerable<PostPart> Get(ThreadPart threadPart, int skip, int count, VersionOptions versionOptions)
    //{
    //    return GetParentPostQuery(threadPart, versionOptions)
    //        .OrderBy(o => o.CreatedUtc)
    //        .ForPart<PostPart>()
    //        .Slice(skip, count)
    //        .ToList();
    //}

    public Task<ForumPart> GetParentForumQuery(PostPart postPart, VersionOptions versionOptions)
    {
        return _session.Query<ForumPart>()
                              .With<ForumIndex>( c=> c.ForumContentItemId == postPart.ForumContentItemId)
                              .FirstOrDefaultAsync();
    }

    public Task<PostPart> GetParentPostQuery(ThreadPart threadPart, VersionOptions versionOptions)
    {
        return _session.Query<PostPart>()
                              .With<PostIndex>(c => c.PostContentItemId == threadPart.PostContentItemId)
                              .FirstOrDefaultAsync();
    }


}

public enum ThreadPostPositional
{
    First,
    Latest
}
