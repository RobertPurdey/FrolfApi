CREATE TABLE [dbo].[frolf_group_invite]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [invitee_id] UNIQUEIDENTIFIER NOT NULL, 
    [frolf_group_id] UNIQUEIDENTIFIER NOT NULL, 
    [inviter_id] UNIQUEIDENTIFIER NOT NULL, 
    [status] TINYINT NOT NULL
)
