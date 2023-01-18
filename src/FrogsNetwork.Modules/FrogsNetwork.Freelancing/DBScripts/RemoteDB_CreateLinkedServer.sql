USE [master]
GO

/****** Object:  LinkedServer [SQL5109.SITE4NOW.NET]    Script Date: 1/18/2023 7:51:41 PM ******/
EXEC master.dbo.sp_addlinkedserver @server = N'SQL5109.SITE4NOW.NET', @srvproduct=N'SQL Server'
 /* For security reasons the linked server remote logins password is changed with ######## */
EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'SQL5109.SITE4NOW.NET',@useself=N'False',@locallogin=NULL,@rmtuser=N'db_a7b8cc_frogsnetworkdev_admin',@rmtpassword='########'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'collation compatible', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'data access', @optvalue=N'true'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'dist', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'pub', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'rpc', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'rpc out', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'sub', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'connect timeout', @optvalue=N'0'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'collation name', @optvalue=null
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'lazy schema validation', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'query timeout', @optvalue=N'0'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'use remote collation', @optvalue=N'true'
GO

EXEC master.dbo.sp_serveroption @server=N'SQL5109.SITE4NOW.NET', @optname=N'remote proc transaction promotion', @optvalue=N'true'
GO


