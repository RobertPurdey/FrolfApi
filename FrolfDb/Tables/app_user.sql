CREATE TABLE [dbo].[app_user]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [login_name] VARCHAR(16) NOT NULL, 
    [password] VARCHAR(100) NOT NULL, 
    [email] VARCHAR(100) NOT NULL, 
    [handle] VARCHAR(20) NOT NULL, 
    [friend_code] VARCHAR(10) NOT NULL
)
