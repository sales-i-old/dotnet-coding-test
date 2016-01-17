CREATE TABLE ToDoItems
(
id uniqueidentifier PRIMARY KEY NOT NULL,
title varchar(255),
[description] text,
complete bit,
parent_task_id uniqueidentifier,
);