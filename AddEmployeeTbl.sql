BEGIN TRANSACTION;
GO

CREATE TABLE [DepartmentsTbl] (
    [Id] int NOT NULL IDENTITY,
    [DeptName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_DepartmentsTbl] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [EmployeeTbl] (
    [EmpID] int NOT NULL IDENTITY,
    [EmpName] nvarchar(max) NOT NULL,
    [Salary] int NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [DeptID] int NOT NULL,
    CONSTRAINT [PK_EmployeeTbl] PRIMARY KEY ([EmpID]),
    CONSTRAINT [FK_EmployeeTbl_DepartmentsTbl_DeptID] FOREIGN KEY ([DeptID]) REFERENCES [DepartmentsTbl] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_EmployeeTbl_DeptID] ON [EmployeeTbl] ([DeptID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240715085434_AddEmployee', N'9.0.0-preview.5.24306.3');
GO

COMMIT;
GO

