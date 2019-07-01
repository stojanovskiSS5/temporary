CREATE PROCEDURE [dbo].[GetUsers]

AS	SELECT 
		UserID,
		Firstname,
		Lastname,
		Email
	FROM 
	[dbo].[Users]