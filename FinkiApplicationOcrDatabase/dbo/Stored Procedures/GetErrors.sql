CREATE PROCEDURE [dbo].[GetErrors]
	@Id int = NULL
AS
	IF(ISNULL(@Id,'') = '')
		(Select * from [dbo].ErrorLog);
	else
		(Select * from [dbo].ErrorLog 
		 Where Id = @Id);