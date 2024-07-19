BEGIN TRANSACTION;
GO

CREATE TABLE [LoginTbl] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(50) NOT NULL,
    [Password] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_LoginTbl] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RegisterTbl] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Contact] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_RegisterTbl] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240711103655_AddAuthTBL', N'9.0.0-preview.5.24306.3');
GO

COMMIT;
GO

