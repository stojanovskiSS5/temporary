CREATE PROCEDURE [dbo].[GetPictureById] 
	@Id int
AS
SELECT 
	[Id],
	[FileName],
	[ImagePath],
	[ImageText],
	[TessarectText],
	[StartProcessingTime],
	[EndProcessingTime],
	[TotalProcessingTime],
	[InvalidWords],
	[InvalidWordsCount],
	[PdfFileId]
FROM  
	[dbo].[Images]
WHERE 
	[Id] = @Id