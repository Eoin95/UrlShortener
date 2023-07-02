USE [master]
GO

CREATE DATABASE UrlShortener;
GO

USE [UrlShortener]
GO

CREATE TABLE [dbo].[UrlPairs](
	[UrlPairID] [int] IDENTITY(1,1) NOT NULL,
	[LongUrl] [nvarchar](1000) NOT NULL,
	[ShortUrl] [nvarchar](50) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UrlPair] PRIMARY KEY CLUSTERED ([UrlPairID] ASC)
)
GO

ALTER TABLE [dbo].[UrlPairs] ADD  CONSTRAINT [UrlPair_CreatedDT]  DEFAULT (getdate()) FOR [CreatedDateTime]
GO

CREATE UNIQUE INDEX idx_url
ON [UrlPairs] ([LongUrl], [ShortUrl]);
GO