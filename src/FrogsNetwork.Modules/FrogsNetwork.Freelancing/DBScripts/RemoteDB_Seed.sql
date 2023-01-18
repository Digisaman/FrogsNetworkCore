---------Countries------------
INSERT INTO [SQL5109.SITE4NOW.NET].[db_a7b8cc_frogsnetworkdev].[dbo].[Country]
           ([Id]
           ,[Name])
     (SELECT Id, [Name] from  [Location].[dbo].[Countries])
GO
----------Regions-----------
INSERT INTO [SQL5109.SITE4NOW.NET].[db_a7b8cc_frogsnetworkdev].[dbo].[Region]
           ([Id]
           ,[Name],
		   [CountryId])
     (SELECT Id, [Name], CountryId from  [Location].[dbo].[Regions])
GO

----------Cities------------
INSERT INTO [SQL5109.SITE4NOW.NET].[db_a7b8cc_frogsnetworkdev].[dbo].City
           ([Id]
           ,[Name],
		   [RegionId])
     (SELECT Id, [Name], RegionId from  [Location].[dbo].[Cities])
GO




select (select count(*) from [SQL5109.SITE4NOW.NET].[db_a7b8cc_frogsnetworkdev].dbo.Country ) as Country_Count,
       (select count(*) from [SQL5109.SITE4NOW.NET].[db_a7b8cc_frogsnetworkdev].dbo.Region ) as Region_Count, 
       ( select count(*) from [SQL5109.SITE4NOW.NET].[db_a7b8cc_frogsnetworkdev].dbo.City ) as City_Count 


