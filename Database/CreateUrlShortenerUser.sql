USE [master]
GO

CREATE LOGIN [UrlShortenSqlUser] WITH PASSWORD=N'Urlshorten123'
GO

USE [UrlShortener]
GO

CREATE USER [UrlShortenSqlUser] FOR LOGIN [UrlShortenSqlUser] WITH DEFAULT_SCHEMA=[dbo]
GO



