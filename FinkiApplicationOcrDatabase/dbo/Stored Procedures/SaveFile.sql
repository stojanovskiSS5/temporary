CREATE PROCEDURE [dbo].[SaveFile]
	@PdfName nvarchar(max) = NULL,
	@PdfPath nvarchar(max) = NULL,
	@ImageText nvarchar(max) = NULL,
	@StartProcessingTime DateTime = NULL,
	@EndProcessingTime DateTime = NULL,
	@ProcessingTime nvarchar(255) = NULL,
	@PagesNumber int = NULL,
	@PdfIdForComparing int = NULL,
	@ShouldCompare tinyint = NULL,
	@Id INT out
AS
	INSERT INTO Files (FileName, FilePath, StartProcessingTime, EndProcessingTime, ProcessingTime, PagesNumber, PdfIdForComparing, ShouldCompare)
	VALUES (@PdfName, @PdfPath, @StartProcessingTime, @EndProcessingTime,@ProcessingTime, @PagesNumber, @PdfIdForComparing, @ShouldCompare)

Set @Id = SCOPE_IDENTITY()