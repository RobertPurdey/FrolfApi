CREATE TABLE [dbo].[game]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [course_id] UNIQUEIDENTIFIER NOT NULL, 
    [frolf_group_id] UNIQUEIDENTIFIER NOT NULL, 
    [created_by] UNIQUEIDENTIFIER NOT NULL, 
    [name] VARCHAR(50) NOT NULL, 
	[state] TINYINT NOT NULL DEFAULT 0, -- Default to InProgress
    [created_date] DATETIME2 NULL, 
)
