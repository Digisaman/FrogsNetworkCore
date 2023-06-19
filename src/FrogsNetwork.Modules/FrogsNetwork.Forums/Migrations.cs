using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Forums.Models;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace FrogsNetwork.Forums;
public class Migrations : DataMigration
{
    IContentDefinitionManager _contentDefinitionManager;

    public Migrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public int Create()
    {
        _contentDefinitionManager.AlterPartDefinition(nameof(ForumPart), part => part
       .Attachable()
       .WithField(nameof(ForumPart.Description), field => field
       .OfType(nameof(TextField))
       .WithDisplayName(nameof(ForumPart.Description))
       .WithSettings(new TextFieldSettings
       {
           Hint = "Forum's Description",
           Required = false
       })
       .WithEditor("TextArea"))

       .WithField(nameof(ForumPart.Body), field => field
       .OfType(nameof(HtmlField))
       .WithDisplayName(nameof(ForumPart.Body))
       .WithSettings(new HtmlFieldSettings
       {
           Hint = "Forum's Body",
           SanitizeHtml = true
       })
       .WithEditor("Wysiwyg")));

        _contentDefinitionManager.AlterTypeDefinition("Forum", type => type
         .WithPart(nameof(ForumPart))
         .Draftable()
         .Versionable()
         .Creatable()
         .Securable()
         .Listable() );



        return 1;
    }

   
}
