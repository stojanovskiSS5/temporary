CREATE TABLE [dbo].[Images] (
	[Id]        INT            IDENTITY (1, 1) NOT NULL,
	[FileName]  NVARCHAR (255)  NULL,
	[ImagePath] NVARCHAR (255) NULL,
	[ImageText] NVARCHAR (MAX) NULL,
	[TessarectText] [nvarchar](max) NULL,
	[StartProcessingTime] [datetime] NULL,
	[EndProcessingTime] [datetime] NULL,
	[TotalProcessingTime]  [nvarchar](255) NULL,
	[InvalidWords] [nvarchar](MAX) NULL,
	[InvalidWordsCount] [int] NULL,
	[PdfFileId] [int] NULL
);

