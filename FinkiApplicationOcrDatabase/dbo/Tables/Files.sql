CREATE TABLE [dbo].[Files] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [FileName]            NVARCHAR (150) NULL,
    [FilePath]            NVARCHAR (250) NULL,
    [StartProcessingTime] DATETIME       NULL,
    [EndProcessingTime]   DATETIME       NULL,
    [ProcessingTime]      NVARCHAR (50)  NULL,
    [PagesNumber]         INT            NULL,
    [PdfIdForComparing]   INT            NULL,
    [ShouldCompare]       TINYINT        NULL
);

