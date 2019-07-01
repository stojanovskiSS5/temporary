CREATE TABLE [dbo].[Users] (
    [UserID]    INT            IDENTITY (1, 1) NOT NULL,
    [Firstname] NVARCHAR (50)  NOT NULL,
    [Lastname]  NVARCHAR (50)  NOT NULL,
    [Email]     NVARCHAR (100) NOT NULL
);

