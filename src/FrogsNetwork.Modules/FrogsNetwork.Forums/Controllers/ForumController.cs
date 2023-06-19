using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Forums.Indexes;
using FrogsNetwork.Forums.Models;
using FrogsNetwork.Forums.ViewModels;
using GraphQL;
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
        public ForumListViewModel ViewModel {get; set;}
        public ForumController(ISession session, IContentManager contentManager)
        {
            _session = session;
            _contentManager = contentManager;
        }
        public async Task<ActionResult> Index()
        {
            this.ViewModel = new ForumListViewModel();

            var forums = await _session
               .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "Forum")
               //.With<ForumPartIndex>( c => c.)
               .ListAsync();

            foreach (var forum in forums)
            {
                await _contentManager.LoadAsync(forum);
            }
            ForumListViewModel forumList = new ForumListViewModel();
            var forumParts = forums.Select(forum => forum.As<ForumPart>());
            ViewModel.Forums = forumParts.Select(c => new ForumPartViewModel
            {
                Description = c.Description.Text,
                Id = c.ContentItem.ContentItemId,
                PostCount = c.PostCount,
                ReplyCount = c.ReplyCount,
                ThreadCount = c.ThreadCount,
                ThreadedPosts = c.ThreadedPosts,
                //Title = c.Title.Content.ToString(),

            });
            return View(this.ViewModel);
        }
        
    }
}
