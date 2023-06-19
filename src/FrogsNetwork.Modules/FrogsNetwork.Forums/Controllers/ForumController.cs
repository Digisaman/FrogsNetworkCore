using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Forums.Indexes;
using FrogsNetwork.Forums.Models;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;

namespace FrogsNetwork.Forums.Controllers
{
    public class ForumController : Controller
    {
        private readonly ISession _session;
        private readonly IContentManager _contentManager;

        public ForumController(ISession session, IContentManager contentManager)
        {
            _session = session;
            _contentManager = contentManager;
        }
        public async Task<IEnumerable<ForumPart>> Index()
        {
            var forums = await _session
               .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "Forum")
               //.With<ForumPartIndex>( c => c.)
               .ListAsync();

            foreach (var forum in forums)
            {
                await _contentManager.LoadAsync(forum);
            }
            return forums.Select(forum => forum.As<ForumPart>());
        }
    }
}
