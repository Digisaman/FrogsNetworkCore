using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Forum.Fields;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Views;

namespace FrogsNetwork.Forum.Settings;
public class ForumFieldSettinfsDriver : ContentPartFieldDefinitionDisplayDriver<ForumField>
{
    public override IDisplayResult Edit(ContentPartFieldDefinition partFieldDefinition)
    {
        return Initialize<ForumFieldSettings>("TaxonomyFieldSettings_Edit", model => partFieldDefinition.PopulateSettings(model))
            .Location("Content");
    }

    public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition partFieldDefinition, UpdatePartFieldEditorContext context)
    {
        var model = new ForumFieldSettings();

        await context.Updater.TryUpdateModelAsync(model, Prefix);

        context.Builder.WithSettings(model);

        return Edit(partFieldDefinition);
    }
}
