using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Forums.Indexes;
using FrogsNetwork.Forums.Models;
using FrogsNetwork.Forums.ViewModels;
using GraphQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.ModelBinding;
using YesSql;

namespace FrogsNetwork.Forums.Controllers
{
    public class ForumController : Controller
    {
        private readonly ISession _session;
        private readonly IContentManager _contentManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        public ForumListViewModel ViewModel { get; set; }
        public ForumController(ISession session,
            IContentManager contentManager,
            IContentItemDisplayManager contentItemDisplayManager,
            IUpdateModelAccessor updateModelAccessor)
        {
            _session = session;
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
            _updateModelAccessor = updateModelAccessor;
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
                PostCount = c.PostCount,
                ReplyCount = c.ReplyCount,
                ThreadCount = c.ThreadCount,
                ThreadedPosts = c.ThreadedPosts,
                Description = c.Description.Text,
                Title = c.Title.Text,
                Id = c.ContentItem.ContentItemId

            });
            return View(this.ViewModel);
        }

        public async Task<ActionResult> Item(string id)
        {
            return this.Redirect($"/Contents/ContentItems/{id}");

        }

        
        public async Task<ActionResult> Create()
        {

            var contentItem = await _contentManager.NewAsync("Forum");
            dynamic model = await _contentItemDisplayManager.BuildEditorAsync(contentItem, _updateModelAccessor.ModelUpdater, true);
            ForumEditViewModel viewModel = new ForumEditViewModel
            {
                ForumId = contentItem.ContentItemId,
                 Item = contentItem,
                ForumEditor = model,
                 ReturnUrl = "/Forum/Index"
            };
            //this.TempData.Add("ViewModel", viewModel);
            this.ViewData.Add("ViewModel", viewModel);
            await _contentItemDisplayManager.UpdateEditorAsync(viewModel.Item, _updateModelAccessor.ModelUpdater, true);
            return View("Create", viewModel);

        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult> CreatePost(ForumEditViewModel viewModel)
        {
            var contentItem = await _contentManager.NewAsync("Forum");
            dynamic model = await _contentItemDisplayManager.BuildEditorAsync(contentItem, _updateModelAccessor.ModelUpdater, true);
            ForumEditViewModel forumEditViewModel = new ForumEditViewModel
            {
                ForumId = contentItem.ContentItemId,
                Item = contentItem,
                ForumEditor = model,
                ReturnUrl = "/Forum/Index"
            };
            //this.TempData.Add("ViewModel", viewModel);
           
            await _contentItemDisplayManager.UpdateEditorAsync(forumEditViewModel.Item, _updateModelAccessor.ModelUpdater, true);
            await _contentManager.PublishAsync(contentItem);
            return View("Create", forumEditViewModel);
        }



        public async Task<ActionResult> Edit(string Id)
        {
            var contentItem = await _session
               .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "Forum" && index.ContentItemId == Id)
               .FirstOrDefaultAsync();
              


           // var forum = await _contentManager.UpdateAsync(contentItem);
            dynamic model = await _contentItemDisplayManager.BuildEditorAsync(contentItem, _updateModelAccessor.ModelUpdater, true);

            ForumEditViewModel viewModel = new ForumEditViewModel
            {
                ForumId = contentItem.ContentItemId,
                Item = contentItem,
                ReturnUrl = "/Forum/Index",
                ForumEditor = model,
            };
            await _contentItemDisplayManager.UpdateEditorAsync(viewModel.Item, _updateModelAccessor.ModelUpdater, true);
            //await _contentManager.PublishAsync(contentItem);
            return View("Edit", viewModel);

        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(ForumEditViewModel viewModel)
        {
            var contentItem = await _session
               .Query<ContentItem, ContentItemIndex>(index => index.ContentType == "Forum" && index.ContentItemId == viewModel.ForumId)
               .FirstOrDefaultAsync();



            // var forum = await _contentManager.UpdateAsync(contentItem);
            dynamic model = await _contentItemDisplayManager.BuildEditorAsync(contentItem, _updateModelAccessor.ModelUpdater, false);

            ForumEditViewModel forunViewModel = new ForumEditViewModel
            {
                ForumId = contentItem.ContentItemId,
                Item = contentItem,
                ReturnUrl = "/Forum/Index",
                ForumEditor = model,
            };
            await _contentItemDisplayManager.UpdateEditorAsync(forunViewModel.Item, _updateModelAccessor.ModelUpdater, true);
            await _contentManager.PublishAsync(contentItem);
            return View("Edit", forunViewModel);
        }
    }
}
