using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using FrogsNetwork.Forum.Models;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using Microsoft.Extensions.Localization;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using FrogsNetwork.Forum.Fields;
using OrchardCore.ContentManagement.Display.Models;
using FrogsNetwork.Forum.Settings;
using OrchardCore.Taxonomies.ViewModels;
using FrogsNetwork.Forum.ViewModels;

namespace FrogsNetwork.Forum.Drivers;

public class ForumFieldDisplayDriver : ContentFieldDisplayDriver<ForumField>
{
    private readonly IContentManager _contentManager;
    private readonly IStringLocalizer S;

    public ForumFieldDisplayDriver(
        IContentManager contentManager,
        IStringLocalizer<ForumFieldDisplayDriver> localizer)
    {
        _contentManager = contentManager;
        S = localizer;
    }

    public override IDisplayResult Display(ForumField field, BuildFieldDisplayContext context)
    {
        return Initialize<DisplayForumFieldViewModel>(GetDisplayShapeType(context), model =>
        {
            model.Field = field;
            model.Part = context.ContentPart;
            model.PartFieldDefinition = context.PartFieldDefinition;
        })
        .Location("Detail", "Content")
        .Location("Summary", "Content");
    }

    public override IDisplayResult Edit(ForumField field, BuildFieldEditorContext context)
    {
        return Initialize<EditForumFieldViewModel>(GetEditorShapeType(context), async model =>
        {
            var settings = context.PartFieldDefinition.GetSettings<ForumFieldSettings>();
            model.Forum = await _contentManager.GetAsync(settings.ForumContentItemId, VersionOptions.Latest);

            if (model.Forum != null)
            {
                //var PostEntries = new List<PostEntry>();
                //TaxonomyFieldDriverHelper.PopulatePostEntries(PostEntries, field, model.Forum.As<ForumPart>().Pos, 0);

                //model.PostEntries = PostEntries;
                //model.UniqueValue = PostEntries.FirstOrDefault(x => x.Selected)?.ContentItemId;
            }

            model.Field = field;
            model.Part = context.ContentPart;
            model.PartFieldDefinition = context.PartFieldDefinition;
        });
    }

    public override async Task<IDisplayResult> UpdateAsync(ForumField field, IUpdateModel updater, UpdateFieldEditorContext context)
    {
        var model = new EditForumFieldViewModel();

        if (await updater.TryUpdateModelAsync(model, Prefix))
        {
            var settings = context.PartFieldDefinition.GetSettings<ForumFieldSettings>();

            field.ForumContentItemId = settings.ForumContentItemId;
            //field.PostContentItemIds = model.PostEntries.Where(x => x.Selected).Select(x => x.ContentItemId).ToArray();

            //if (settings.Unique && !String.IsNullOrEmpty(model.UniqueValue))
            //{
            //    field.PostContentItemIds = new[] { model.UniqueValue };
            //}

            //if (settings.Required && field.PostContentItemIds.Length == 0)
            //{
            //    updater.ModelState.AddModelError(
            //        nameof(EditForumFieldViewModel.PostEntries),
            //        S["A value is required for '{0}'", context.PartFieldDefinition.DisplayName()]);
            //}
        }

        return Edit(field, context);
    }
}
//public class PostPartDriver : ContentPartDriver<PostPart>
//{
//    private readonly IWorkContextAccessor _workContextAccessor;
//    private readonly IEnumerable<IHtmlFilter> _htmlFilters;
//    private readonly RequestContext _requestContext;
//    private readonly IContentManager _contentManager;

//    private const string TemplateName = "Parts.Threads.Post.Body";

//    public PostPartDriver(IWorkContextAccessor workContextAccessor,
//        IEnumerable<IHtmlFilter> htmlFilters,
//        RequestContext requestContext,
//        IContentManager contentManager)
//    {
//        _workContextAccessor = workContextAccessor;
//        _htmlFilters = htmlFilters;
//        _requestContext = requestContext;
//        _contentManager = contentManager;
//    }

//    protected override string Prefix
//    {
//        get { return "PostPart"; }
//    }

//    protected override DriverResult Display(PostPart part, string displayType, dynamic shapeHelper)
//    {
//        return Combined(
//            ContentShape("Parts_Threads_Post_Body",
//                         () => {
//                             var bodyText = _htmlFilters.Aggregate(part.Text, (text, filter) => filter.ProcessContent(text, GetFlavor(part)));
//                             return shapeHelper.Parts_Threads_Post_Body(Html: new HtmlString(bodyText));
//                         }),
//            ContentShape("Parts_Threads_Post_Body_Summary",
//                         () => {
//                             var pager = new ThreadPager(_workContextAccessor.GetContext().CurrentSite, part.ThreadPart.PostCount);
//                             var bodyText = _htmlFilters.Aggregate(part.Text, (text, filter) => filter.ProcessContent(text, GetFlavor(part)));
//                             return shapeHelper.Parts_Threads_Post_Body_Summary(Html: new HtmlString(bodyText), Pager: pager);
//                         }),
//            ContentShape("Parts_Post_Manage", () => {
//                var newPost = _contentManager.New<PostPart>(part.ContentItem.ContentType);
//                newPost.ThreadPart = part.ThreadPart;
//                return shapeHelper.Parts_Post_Manage(ContentPart: part, NewPost: newPost);
//            }),
//            ContentShape("Parts_Thread_Post_Metadata_SummaryAdmin", () =>
//                shapeHelper.Parts_Thread_Post_Metadata_SummaryAdmin(ContentPart: part))
//            );
//    }

//    protected override DriverResult Editor(PostPart part, dynamic shapeHelper)
//    {
//        var model = BuildEditorViewModel(part, _requestContext);
//        return Combined(ContentShape("Parts_Threads_Post_Body_Edit",
//                            () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: model, Prefix: Prefix)));
//    }

//    protected override DriverResult Editor(PostPart part, IUpdateModel updater, dynamic shapeHelper)
//    {
//        var model = BuildEditorViewModel(part, _requestContext);
//        updater.TryUpdateModel(model, Prefix, null, null);

//        return Combined(ContentShape("Parts_Threads_Post_Body_Edit",
//                            () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: model, Prefix: Prefix)));
//    }

//    private static PostBodyEditorViewModel BuildEditorViewModel(PostPart part, RequestContext requestContext)
//    {
//        return new PostBodyEditorViewModel
//        {
//            PostPart = part,
//            EditorFlavor = GetFlavor(part),
//        };
//    }

//    private static string GetFlavor(PostPart part)
//    {
//        var typePartSettings = part.Settings.GetModel<PostTypePartSettings>();
//        return (typePartSettings != null && !string.IsNullOrWhiteSpace(typePartSettings.Flavor))
//                   ? typePartSettings.Flavor
//                   : part.PartDefinition.Settings.GetModel<PostPartSettings>().FlavorDefault;
//    }

//    protected override void Importing(PostPart part, ImportContentContext context)
//    {
//        var format = context.Attribute(part.PartDefinition.Name, "Format");
//        if (format != null)
//        {
//            part.Format = format;
//        }

//        var repliedOn = context.Attribute(part.PartDefinition.Name, "RepliedOn");
//        if (repliedOn != null)
//        {
//            part.RepliedOn = context.GetItemFromSession(repliedOn).Id;
//        }

//        var text = context.Attribute(part.PartDefinition.Name, "Text");
//        if (text != null)
//        {
//            part.Text = text;
//        }
//    }

//    protected override void Exporting(PostPart part, ExportContentContext context)
//    {
//        context.Element(part.PartDefinition.Name).SetAttributeValue("Format", part.Format);

//        if (part.RepliedOn != null)
//        {
//            var repliedOnIdentity = _contentManager.GetItemMetadata(_contentManager.Get(part.RepliedOn.Value)).Identity;
//            context.Element(part.PartDefinition.Name).SetAttributeValue("RepliedOn", repliedOnIdentity.ToString());
//        }

//        context.Element(part.PartDefinition.Name).SetAttributeValue("Text", part.Text);
//    }
//}
