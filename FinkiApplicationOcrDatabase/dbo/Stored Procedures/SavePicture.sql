CREATE PROCEDURE [dbo].[SavePicture]
	@FileName nvarchar(max) = NULL,
	@ImagePath nvarchar(max) = NULL,
	@ImageText nvarchar(max) = NULL,
	@InvalidWords nvarchar(max) = NULL,
	@InvalidWordsCount int = NULL,
	@StartProcessingTime DateTime = NULL,
	@EndProcessingTime DateTime = NULL,
	@ProcessingTime nvarchar(255) = NULL,
	@TesseractImageText nvarchar(max) = NULL,
	@PdfFileId int = NULL,
	@Id INT out
AS
	INSERT INTO Images (FileName,ImagePath,ImageText,TessarectText,StartProcessingTime,EndProcessingTime,TotalProcessingTime,InvalidWords,InvalidWordsCount, PdfFileId)
	VALUES (@FileName, @ImagePath, @ImageText, @TesseractImageText,@StartProcessingTime,@EndProcessingTime,@ProcessingTime,@InvalidWords,@InvalidWordsCount, @PdfFileId)
	
	SET @Id = SCOPE_IDENTITY()