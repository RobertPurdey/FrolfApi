CREATE TABLE [dbo].[player]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [app_user_id] UNIQUEIDENTIFIER NOT NULL, 
	[frolf_group_id] UNIQUEIDENTIFIER NOT NULL, 
    [group_role] TINYINT NOT NULL, 
    [handle] VARCHAR(15) NOT NULL, 
)
