CREATE TABLE [dbo].[app_user_refresh_token]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [user_id] UNIQUEIDENTIFIER NOT NULL, 
    [token] NVARCHAR(100) NOT NULL, 
    [serialized_ticket] NVARCHAR(MAX) NOT NULL, 
    [issued_on] DATETIME NOT NULL, 
    [expires_on] DATETIME NOT NULL
)
