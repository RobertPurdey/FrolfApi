CREATE TABLE [dbo].[frolf_group_invite]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [app_user_id] UNIQUEIDENTIFIER NOT NULL, 
    [frolf_group_id] UNIQUEIDENTIFIER NOT NULL, 
    [created_by] UNIQUEIDENTIFIER NOT NULL, 
    [status] TINYINT NOT NULL
)
