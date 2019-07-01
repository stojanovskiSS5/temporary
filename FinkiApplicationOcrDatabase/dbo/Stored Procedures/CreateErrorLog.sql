CREATE PROCEDURE [dbo].[CreateErrorLog]
	@ExceptionReason nvarchar(max) = NULL,
	@ExceptionDate datetime,
	@Method nvarchar(max),
	@Parametars nvarchar(max) = NULL
AS
	INSERT INTO [dbo].[ErrorLog] (ExceptionReason,Method,Parametars,ExceptionDate)
	VALUES (@ExceptionReason,@Method, @Parametars, @ExceptionDate)