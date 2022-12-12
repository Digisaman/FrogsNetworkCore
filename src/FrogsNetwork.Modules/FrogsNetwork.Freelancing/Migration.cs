using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Freelancing.Models;
using OrchardCore.Data.Migration;
using YesSql.Sql;

namespace FrogsNetwork.Freelancing;
public class Migrations : DataMigration
{

    // This is a sequenced migration. On a new schemas this is complete after UpdateFrom2.
    public int Create()
    {
        SchemaBuilder.CreateTable(nameof(Country), table => table
            .Column<int>(nameof(Country.Id), col => col.PrimaryKey())
            .Column<string>(nameof(Country.Name), c => c.WithLength(50))
            
        );

        

        return 1;
    }

    public int UpdateFrom1()
    {
        SchemaBuilder.CreateTable(nameof(Region), table => table
            .Column<int>(nameof(Region.Id), col => col.PrimaryKey())
            .Column<string>(nameof(Region.Name), c => c.WithLength(50))
            .Column<int>(nameof(Region.CountryId))

            ).
            CreateForeignKey("FK_Country", nameof(Region), new string[] { nameof(Region.CountryId) }, nameof(Country), new string[] { nameof(Country.Id) });
        return 2;
    }

    public int UpdateFrom2()
    {
        SchemaBuilder.CreateTable(nameof(City), table => table
            .Column<int>(nameof(City.Id), col => col.PrimaryKey())
            .Column<string>(nameof(City.Name), c => c.WithLength(50))
            .Column<int>(nameof(City.RegionId))

            ).
            CreateForeignKey("FK_Region", nameof(City), new string[] { nameof(City.RegionId) }, nameof(Region), new string[] { nameof(Region.Id) });
        return 3;
    }


}
