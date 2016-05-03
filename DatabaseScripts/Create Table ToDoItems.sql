CREATE TABLE [dbo].[ToDoItems](
[id] [uniqueidentifier] NOT NULL,
[title] [nvarchar](250) NOT NULL,
[description] [nvarchar](max) NULL,
[complete] [bit] NOT NULL,
 CONSTRAINT [PK_ToDoItems] PRIMARY KEY CLUSTERED 
(
[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ToDoItems] ADD  CONSTRAINT [DF_ToDoItems_complete]  DEFAULT ((0)) FOR [complete]
GO