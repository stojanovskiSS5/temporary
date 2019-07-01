CREATE TABLE [dbo].[ErrorLog] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Parametars]      NVARCHAR (MAX) NULL,
    [ExceptionReason] NVARCHAR (MAX) NOT NULL,
    [Method]          NVARCHAR (MAX) NOT NULL,
    [ExceptionDate]   DATETIME       DEFAULT (getdate()) NULL
);



