using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Forums.Models;
using FrogsNetwork.Forums.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace FrogsNetwork.Forums.Drivers;

public class ForumPartDisplayDriver : ContentPartDisplayDriver<ForumPart>
{
    public override IDisplayResult Display(ForumPart part, BuildPartDisplayContext context) =>
        Initialize<ForumPartViewModel>(
            GetDisplayShapeType(context),
            viewModel => PopulateViewModel(part, viewModel))
        .Location("Detail", "Content:5")
        .Location("Summary", "Content:5");

    public override IDisplayResult Edit(ForumPart part, BuildPartEditorContext context) =>
        Initialize<ForumPartViewModel>(
            GetEditorShapeType(context),
            viewModel => PopulateViewModel(part, viewModel))
        .Location("Content:5");


    //{
    //    return Initialize<ForumPartViewModel>("ForumPart.Edit",
    //        viewModel => {
    //            GetEditorShapeType(context);
    //            PopulateViewModel(part, viewModel);
    //        } );
    //}
    public override async Task<IDisplayResult> UpdateAsync(ForumPart part, IUpdateModel updater, UpdatePartEditorContext context)
    {
        var viewModel = new ForumPartViewModel();

        await updater.TryUpdateModelAsync(viewModel, Prefix);

        //part.Description = viewModel.Description;
        part.Weight = viewModel.Weight;
        part.PostCount = viewModel.PostCount;
        part.ThreadCount = viewModel.ThreadCount;
        part.ThreadedPosts = viewModel.ThreadedPosts;
        
        
        return await EditAsync(part, context);
    }

    private static void PopulateViewModel(ForumPart part, ForumPartViewModel viewModel)
    {
        //viewModel.Description = part.Description;
        viewModel.Weight = part.Weight;
        viewModel.PostCount = part.PostCount;
        viewModel.ReplyCount = part.ReplyCount;
        viewModel.ThreadCount = part.ThreadCount;
        viewModel.ThreadedPosts = part.ThreadedPosts;
        viewModel.Id = part.ContentItem.ContentItemId;
                

    }
}
