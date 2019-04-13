CREATE TABLE [dbo].[frolf_group]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [name] VARCHAR(50) NOT NULL, 
    [created_by] UNIQUEIDENTIFIER NOT NULL, 
    [created_date] DATETIME2 NULL
)
