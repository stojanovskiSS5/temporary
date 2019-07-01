CREATE PROCEDURE [dbo].[InsertUser]
	@FirstName  NVARCHAR(50),
	@LastName  NVARCHAR(50),
	@Email NVARCHAR(100)
AS
	INSERT INTO [dbo].Users(Firstname,Lastname,Email) 
	VALUES (@FirstName,@LastName,@Email)

	SELECT * from [dbo].Users where UserID = @@IDENTITY