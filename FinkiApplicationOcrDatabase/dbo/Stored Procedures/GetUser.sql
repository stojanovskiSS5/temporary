CREATE PROCEDURE [dbo].[GetUser] 
	@Firstname nvarchar(50),
	@Lastname nvarchar(50),
	@Email nvarchar(100)
AS
SELECT 
	[UserID],
	[Firstname],
	[Lastname],
	[Email]
FROM  
	[dbo].[Users]
WHERE 
	[Firstname] = @Firstname and
	[Lastname] = @Lastname and
	[Email] = @Email