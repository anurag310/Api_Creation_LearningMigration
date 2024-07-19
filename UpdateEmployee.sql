BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EmployeeTbl]') AND [c].[name] = N'EmpName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [EmployeeTbl] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [EmployeeTbl] ALTER COLUMN [EmpName] nvarchar(max) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240715091318_UpdateEmployee', N'9.0.0-preview.5.24306.3');
GO

COMMIT;
GO

