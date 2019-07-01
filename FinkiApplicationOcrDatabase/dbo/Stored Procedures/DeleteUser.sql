CREATE PROCEDURE [dbo].[DeleteUser]
	@Id  int,
	@Email NVARCHAR(100)
AS
	DELETE FROM 
	[dbo].Users
	where [dbo].Users.UserID = @Id 
	and [dbo].Users.Email = @Email