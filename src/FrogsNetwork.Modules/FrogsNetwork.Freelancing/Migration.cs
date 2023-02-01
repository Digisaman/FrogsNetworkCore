using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogsNetwork.Freelancing.Models;
using OrchardCore.Data.Migration;
using OrchardCore.Users.Indexes;
using OrchardCore.Users.Models;
using YesSql.Sql;

namespace FrogsNetwork.Freelancing;
public class Migrations : DataMigration
{

    // This is a sequenced migration. On a new schemas this is complete after UpdateFrom2.
    public int Create()
    {
        SchemaBuilder.CreateTable(nameof(Country), table => table
            .Column<int>(nameof(Country.Id), col => col.PrimaryKey())
            .Column<string>(nameof(Country.Name), c => c.NotNull().WithLength(50))
        );


        SchemaBuilder.CreateTable(nameof(Region), table => table
            .Column<int>(nameof(Region.Id), col => col.PrimaryKey())
            .Column<string>(nameof(Region.Name), c => c.NotNull().WithLength(50))
            .Column<int>(nameof(Region.CountryId), c => c.NotNull())

            ).
            CreateForeignKey("FK_Region_Country", nameof(Region), new string[] { nameof(Region.CountryId) }, nameof(Country), new string[] { nameof(Country.Id) });

        SchemaBuilder.CreateTable(nameof(City), table => table
            .Column<int>(nameof(City.Id), col => col.PrimaryKey())
            .Column<string>(nameof(City.Name), c => c.NotNull().WithLength(50))
            .Column<int>(nameof(City.RegionId), c => c.NotNull()))
            .CreateForeignKey("FK_City_Region", nameof(City), new string[] { nameof(City.RegionId) }, nameof(Region), new string[] { nameof(Region.Id) });


        SchemaBuilder.CreateTable(nameof(FreelancerUser), table => table
            .Column<int>(nameof(FreelancerUser.Id), col => col.PrimaryKey().Identity())
            .Column<string>(nameof(FreelancerUser.UserId), c => c.NotNull().WithLength(50))
            .Column<int>(nameof(FreelancerUser.CountryId), c => c.Nullable())
            .Column<int>(nameof(FreelancerUser.RegionId), c => c.Nullable())
            .Column<int>(nameof(FreelancerUser.CityId), c => c.Nullable())
            .Column<DateTime>(nameof(FreelancerUser.BirthDate))
            .Column<string>(nameof(FreelancerUser.Lat), c => c.WithLength(50))
            .Column<string>(nameof(FreelancerUser.Long), c => c.WithLength(50))
            .Column<string>(nameof(FreelancerUser.VAT), c => c.WithLength(50))
            .Column<string>(nameof(FreelancerUser.Address), c => c.WithLength(100))
            .Column<string>(nameof(FreelancerUser.PostalCode), c => c.WithLength(50))
            .Column<string>(nameof(FreelancerUser.Tel), c => c.WithLength(50))
            .Column<string>(nameof(FreelancerUser.FirstName), c => c.WithLength(50))
            .Column<string>(nameof(FreelancerUser.LastName), c => c.WithLength(50))
            .Column<string>(nameof(FreelancerUser.Mobile), c => c.WithLength(50))
            .Column<string>(nameof(FreelancerUser.Website), c => c.WithLength(50)))
            .CreateForeignKey("FK_Freelancer_Country", nameof(FreelancerUser), new string[] { nameof(FreelancerUser.CountryId) }, nameof(Country), new string[] { nameof(Country.Id) })
            .CreateForeignKey("FK_Freelancer_Region", nameof(FreelancerUser), new string[] { nameof(FreelancerUser.RegionId) }, nameof(Region), new string[] { nameof(Region.Id) })
            .CreateForeignKey("FK_Freelancer_City", nameof(FreelancerUser), new string[] { nameof(FreelancerUser.CityId) }, nameof(City), new string[] { nameof(City.Id) });


        SchemaBuilder.CreateTable(nameof(CompanyUser), table => table
            .Column<int>(nameof(CompanyUser.Id), col => col.PrimaryKey().Identity())
            .Column<string>(nameof(CompanyUser.UserId), c => c.NotNull().WithLength(50))
            .Column<int>(nameof(CompanyUser.CountryId), c => c.Nullable())
            .Column<int>(nameof(CompanyUser.RegionId), c => c.Nullable())
            .Column<int>(nameof(CompanyUser.CityId), c => c.Nullable())
            .Column<string>(nameof(CompanyUser.Lat), c => c.WithLength(50))
            .Column<string>(nameof(CompanyUser.Long), c => c.WithLength(50))
            .Column<string>(nameof(CompanyUser.VAT), c => c.WithLength(50))
            .Column<string>(nameof(CompanyUser.Address), c => c.WithLength(100))
            .Column<string>(nameof(CompanyUser.PostalCode), c => c.WithLength(50))
            .Column<string>(nameof(CompanyUser.Activities), c => c.WithLength(50))
            .Column<string>(nameof(CompanyUser.CompanyName), c => c.WithLength(50))
            .Column<string>(nameof(CompanyUser.CompanyTel), c => c.WithLength(50))
            .Column<string>(nameof(CompanyUser.Website), c => c.WithLength(50))
            .Column<string>(nameof(CompanyUser.ContactPersonPosition), c => c.WithLength(50))
            .Column<string>(nameof(CompanyUser.ContactPersonName), c => c.WithLength(50)))
            .CreateForeignKey("FK_Company_Country", nameof(CompanyUser), new string[] { nameof(CompanyUser.CountryId) }, nameof(Country), new string[] { nameof(Country.Id) })
            .CreateForeignKey("FK_Company_Region", nameof(CompanyUser), new string[] { nameof(CompanyUser.RegionId) }, nameof(Region), new string[] { nameof(Region.Id) })
            .CreateForeignKey("FK_Company_City", nameof(CompanyUser), new string[] { nameof(CompanyUser.CityId) }, nameof(City), new string[] { nameof(City.Id) });


        return 1;
    }

    public int UpdateFrom1()
    {
        SchemaBuilder.AlterTable(nameof(City), table => table
            .DropColumn(nameof(City.Name)));
        SchemaBuilder.AlterTable(nameof(City), table => table
            .AddColumn<string>(nameof(City.Name), c => c.NotNull().WithLength(100)));


        return 2;
    }

    public int UpdateFrom2()
    {
        SchemaBuilder.CreateTable(nameof(Language), table => table
            .Column<int>(nameof(Language.Id), col => col.PrimaryKey().Identity())
            .Column<string>(nameof(Language.Name), c => c.NotNull().WithLength(50))
        );

        SchemaBuilder.CreateTable(nameof(Nationality), table => table
           .Column<int>(nameof(Nationality.Id), col => col.PrimaryKey().Identity())
           .Column<string>(nameof(Nationality.Name), c => c.NotNull().WithLength(50))
       );


        return 3;
    }

    public int UpdateFrom3()
    {
        SchemaBuilder.CreateTable(nameof(FreelancerNationality), table => table
            .Column<int>(nameof(FreelancerNationality.Id), col => col.PrimaryKey().Identity())
            .Column<int>(nameof(FreelancerNationality.FreelancerId), c => c.NotNull())
            .Column<int>(nameof(FreelancerNationality.NationalityId), c => c.NotNull())
        )
        .CreateForeignKey("FK_FreelancerNationality_Freelancer", nameof(FreelancerNationality), new string[] { nameof(FreelancerNationality.FreelancerId) }, nameof(FreelancerUser), new string[] { nameof(FreelancerUser.Id) })
        .CreateForeignKey("FK_FreelancerNationality_Nationality", nameof(FreelancerNationality), new string[] { nameof(FreelancerNationality.NationalityId) }, nameof(Nationality), new string[] { nameof(Nationality.Id) });


        SchemaBuilder.CreateTable(nameof(FreelancerLanguage), table => table
            .Column<int>(nameof(FreelancerLanguage.Id), col => col.PrimaryKey().Identity())
            .Column<int>(nameof(FreelancerLanguage.FreelancerId), c => c.NotNull())
            .Column<int>(nameof(FreelancerLanguage.LanguageId), c => c.NotNull())
        )
        .CreateForeignKey("FK_FreelancerLanguage_Freelancer", nameof(FreelancerLanguage), new string[] { nameof(FreelancerLanguage.FreelancerId) }, nameof(FreelancerUser), new string[] { nameof(FreelancerUser.Id) })
        .CreateForeignKey("FK_FreelancerLanguage_Language", nameof(FreelancerLanguage), new string[] { nameof(FreelancerLanguage.LanguageId) }, nameof(Language), new string[] { nameof(Language.Id) });
        return 4;
    }

    public int UpdateFrom4()
    {
        SchemaBuilder.AlterTable(nameof(FreelancerLanguage), table => table
           .AddColumn<LanguageLevel>(nameof(FreelancerLanguage.Level), c => c.NotNull()));

        SchemaBuilder.CreateTable(nameof(FreelancerCertificate), table => table
            .Column<int>(nameof(FreelancerCertificate.Id), col => col.PrimaryKey().Identity())
            .Column<int>(nameof(FreelancerCertificate.FreelancerId), c => c.NotNull())
            .Column<string>(nameof(FreelancerCertificate.Certificate), c => c.NotNull().WithLength(100))
            .Column<string>(nameof(FreelancerCertificate.Organization), c => c.NotNull().WithLength(100))
            .Column<string>(nameof(FreelancerCertificate.Description), c => c.WithLength(200))
        )
        .CreateForeignKey($"FK_FreelancerCertificate_Freelancer", nameof(FreelancerCertificate), new string[] { nameof(FreelancerCertificate.FreelancerId) }, nameof(FreelancerUser), new string[] { nameof(FreelancerUser.Id) });

        SchemaBuilder.CreateTable(nameof(FreelancerEducation), table => table
            .Column<int>(nameof(FreelancerEducation.Id), col => col.PrimaryKey().Identity())
            .Column<int>(nameof(FreelancerEducation.FreelancerId), c => c.NotNull())
            .Column<int>(nameof(FreelancerEducation.CountryId))
            .Column<int>(nameof(FreelancerEducation.EndYear))
            .Column<string>(nameof(FreelancerEducation.City), c => c.WithLength(100))
            .Column<string>(nameof(FreelancerEducation.Field), c => c.WithLength(100))
            .Column<string>(nameof(FreelancerEducation.Degree), c => c.NotNull().WithLength(100))
            .Column<string>(nameof(FreelancerEducation.School), c => c.NotNull().WithLength(200))
        )
        .CreateForeignKey("FK_FreelancerEducation_Freelancer", nameof(FreelancerEducation), new string[] { nameof(FreelancerEducation.FreelancerId) }, nameof(FreelancerUser), new string[] { nameof(FreelancerUser.Id) })
        .CreateForeignKey("FK_FreelancerEducation_Country", nameof(FreelancerEducation), new string[] { nameof(FreelancerEducation.CountryId) }, nameof(Country), new string[] { nameof(Country.Id) });


        SchemaBuilder.CreateTable(nameof(FreelancerExpertise), table => table
           .Column<int>(nameof(FreelancerExpertise.Id), col => col.PrimaryKey().Identity())
           .Column<int>(nameof(FreelancerExpertise.FreelancerId), c => c.NotNull())
           .Column<string>(nameof(FreelancerExpertise.ExpertiseId), c => c.NotNull().WithLength(100))
           .Column<int>(nameof(FreelancerExpertise.LevelId), c => c.NotNull().WithLength(100))
       )
       .CreateForeignKey("FK_FreelancerExpertise_Freelancer", nameof(FreelancerExpertise), new string[] { nameof(FreelancerExpertise.FreelancerId) }, nameof(FreelancerUser), new string[] { nameof(FreelancerUser.Id) });

        SchemaBuilder.CreateTable(nameof(FreelancerService), table => table
           .Column<int>(nameof(FreelancerService.Id), col => col.PrimaryKey().Identity())
           .Column<int>(nameof(FreelancerService.FreelancerId), c => c.NotNull())
           .Column<string>(nameof(FreelancerService.ServiceId), c => c.NotNull().WithLength(100))
           .Column<int>(nameof(FreelancerService.LevelId), c => c.NotNull().WithLength(100))
       )
       .CreateForeignKey("FK_FreelancerService_Freelancer", nameof(FreelancerService), new string[] { nameof(FreelancerService.FreelancerId) }, nameof(FreelancerUser), new string[] { nameof(FreelancerUser.Id) });


        return 5;
    }




}
