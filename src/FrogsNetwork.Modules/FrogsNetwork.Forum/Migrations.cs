
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Forum.Models;
using OrchardCore.Data.Migration;

using YesSql.Sql;

namespace FrogsNetwork.Forum;
public class Migrations : DataMigration
{

    // This is a sequenced migration. On a new schemas this is complete after UpdateFrom2.
    public int Create()
    {
        SchemaBuilder.CreateTable(nameof(Models.Forum), table => table
            .Column<int>(nameof(Models.Forum.Id), col => col.PrimaryKey())
            .Column<string>(nameof(Models.Forum.Title), c => c.NotNull().WithLength(50))
        );
        return 1;
    }
}
