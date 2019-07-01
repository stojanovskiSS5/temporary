CREATE TABLE [dbo].[SessionLog] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserID]       INT            NOT NULL,
    [JwtToken]     NVARCHAR (255) NOT NULL,
    [DateInserted] DATETIME       DEFAULT (getdate()) NOT NULL,
    [ExpiryDate]   DATETIME       NOT NULL,
    [Browser]      NVARCHAR (255) NULL,
    [IpAddress]    NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

