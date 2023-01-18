---------Countries------------
INSERT INTO [FrogsNetworkDev].[dbo].[Country]
           ([Id]
           ,[Name])
     (SELECT Id, [Name] from  [Location].[dbo].[Countries])
GO
----------Regions------------
INSERT INTO [FrogsNetworkDev].[dbo].[Region]
           ([Id]
           ,[Name],
		   [CountryId])
     (SELECT Id, [Name], CountryId from  [Location].[dbo].[Regions])
GO
----------Cities------------
INSERT INTO [FrogsNetworkDev].[dbo].City
           ([Id]
           ,[Name],
		   [RegionId])
     (SELECT Id, [Name], RegionId from  [Location].[dbo].[Cities])
GO


select (select count(*) from FrogsNetworkDev.dbo.Country ) as Country_Count,
       (select count(*) from FrogsNetworkDev.dbo.Region ) as Region_Count, 
       ( select count(*) from FrogsNetworkDev.dbo.City ) as City_Count 
