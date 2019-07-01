CREATE PROCEDURE [dbo].[CreateSessionLog]
	@userID nvarchar(50),
	@jwtToken nvarchar(255),
	@dateInserted datetime,
	@expiryDate datetime,
	@browser nvarchar(255) = null,
	@ipAddress nvarchar(100) = null
AS
	INSERT INTO SessionLog (UserID,JwtToken,DateInserted,ExpiryDate,Browser,IpAddress)
	VALUES (@userID,@jwtToken,@dateInserted,@expiryDate,@browser,@ipAddress)
RETURN 0