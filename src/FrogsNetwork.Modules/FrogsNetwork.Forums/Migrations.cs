using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        // This code will be run when the feature is enabled
        _contentDefinitionManager.AlterTypeDefinition("Forum", type => type
         .WithPart("ForumPart")
    // content items of this type can have drafts
    .Draftable()
    // content items versions of this type have saved
    .Versionable()
    // this content type appears in the New menu section
    .Creatable()
    // permissions can be applied specifically to instances of this type
    .Securable()
);

        return 1;
    }
}
