IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FName] nvarchar(max) NOT NULL,
        [LName] nvarchar(max) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [Employees] (
        [empID] int NOT NULL IDENTITY,
        [fName] varchar(50) NULL,
        [lName] varchar(50) NULL,
        [phone] varchar(15) NULL,
        [email] varchar(100) NULL,
        CONSTRAINT [PK__Employee__AFB3EC6D642F11E3] PRIMARY KEY ([empID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [Users] (
        [ID] int NOT NULL IDENTITY,
        [fName] varchar(50) NULL,
        [lName] varchar(50) NULL,
        [phone] varchar(15) NULL,
        [email] varchar(100) NULL,
        [type] varchar(20) NULL,
        [password] varchar(50) NULL,
        CONSTRAINT [PK__Users__3214EC27442F6DEB] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [About] (
        [aboutNo] int NOT NULL IDENTITY,
        [empID] int NULL,
        [title] int NULL,
        [text] varchar(max) NULL,
        CONSTRAINT [PK__About__AB3FDC2C325B1618] PRIMARY KEY ([aboutNo]),
        CONSTRAINT [FK__About__empID__48CFD27E] FOREIGN KEY ([empID]) REFERENCES [Employees] ([empID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [Banners] (
        [banarNo] int NOT NULL IDENTITY,
        [empID] int NULL,
        [subTitle] varchar(100) NULL,
        [img] varbinary(max) NULL,
        CONSTRAINT [PK__Banners__E1C7EEED86D5AABA] PRIMARY KEY ([banarNo]),
        CONSTRAINT [FK__Banners__empID__5441852A] FOREIGN KEY ([empID]) REFERENCES [Employees] ([empID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [PriceAndRate] (
        [prNo] int NOT NULL IDENTITY,
        [empID] int NULL,
        [text] varchar(max) NULL,
        CONSTRAINT [PK__PriceAnd__46655E2E38A279F4] PRIMARY KEY ([prNo]),
        CONSTRAINT [FK__PriceAndR__empID__4E88ABD4] FOREIGN KEY ([empID]) REFERENCES [Employees] ([empID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [PrivacyAndPolicy] (
        [ppNo] int NOT NULL IDENTITY,
        [empID] int NULL,
        [text] varchar(max) NULL,
        CONSTRAINT [PK__PrivacyA__41E4AB434FEF2435] PRIMARY KEY ([ppNo]),
        CONSTRAINT [FK__PrivacyAn__empID__4BAC3F29] FOREIGN KEY ([empID]) REFERENCES [Employees] ([empID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [Salaries] (
        [salaryNo] int NOT NULL IDENTITY,
        [empID] int NULL,
        [Money] decimal(10,2) NULL,
        [date] date NULL,
        CONSTRAINT [PK__Salaries__336377C79269CBC0] PRIMARY KEY ([salaryNo]),
        CONSTRAINT [FK__Salaries__empID__45F365D3] FOREIGN KEY ([empID]) REFERENCES [Employees] ([empID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [SocialMedia] (
        [socialMedNo] int NOT NULL IDENTITY,
        [empID] int NULL,
        [name] varchar(50) NULL,
        [img] varbinary(max) NULL,
        CONSTRAINT [PK__SocialMe__37AA6055C1FEBF49] PRIMARY KEY ([socialMedNo]),
        CONSTRAINT [FK__SocialMed__empID__571DF1D5] FOREIGN KEY ([empID]) REFERENCES [Employees] ([empID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [TermsAndConditions] (
        [tcNo] int NOT NULL IDENTITY,
        [empID] int NULL,
        [text] varchar(max) NULL,
        CONSTRAINT [PK__TermsAnd__E07867518DB768ED] PRIMARY KEY ([tcNo]),
        CONSTRAINT [FK__TermsAndC__empID__5165187F] FOREIGN KEY ([empID]) REFERENCES [Employees] ([empID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [Titles] (
        [titleID] int NOT NULL IDENTITY,
        [empID] int NULL,
        [titleName] varchar(50) NULL,
        CONSTRAINT [PK__Titles__4D72D6AAEEDF5763] PRIMARY KEY ([titleID]),
        CONSTRAINT [FK__Titles__empID__4316F928] FOREIGN KEY ([empID]) REFERENCES [Employees] ([empID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [Biddings] (
        [biddingNo] int NOT NULL IDENTITY,
        [userID] int NULL,
        [title] varchar(100) NULL,
        [itemNo] nvarchar(max) NULL,
        CONSTRAINT [PK__Biddings__B58ECEC7767E4C79] PRIMARY KEY ([biddingNo]),
        CONSTRAINT [FK__Biddings__userID__32E0915F] FOREIGN KEY ([userID]) REFERENCES [Users] ([ID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [Mails] (
        [mailNo] int NOT NULL IDENTITY,
        [userID] int NULL,
        [emailMsg] varchar(max) NULL,
        CONSTRAINT [PK__Mails__F5CD605BCDDC429C] PRIMARY KEY ([mailNo]),
        CONSTRAINT [FK__Mails__userID__300424B4] FOREIGN KEY ([userID]) REFERENCES [Users] ([ID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [Admin] (
        [empID] int NULL,
        [titleID] int NULL,
        [password] varchar(50) NULL,
        [email] varchar(100) NULL,
        CONSTRAINT [FK__Admin__empID__5FB337D6] FOREIGN KEY ([empID]) REFERENCES [Employees] ([empID]),
        CONSTRAINT [FK__Admin__titleID__60A75C0F] FOREIGN KEY ([titleID]) REFERENCES [Titles] ([titleID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [Items] (
        [id] int NOT NULL IDENTITY,
        [userID] int NULL,
        [biddingNo] int NULL,
        [name] varchar(100) NULL,
        [type] varchar(50) NULL,
        [history] varchar(max) NULL,
        [price] decimal(10,2) NULL,
        [disc] varchar(max) NULL,
        [health] varchar(50) NULL,
        [checkedDate] datetime NULL,
        CONSTRAINT [PK__Items__3213E83F9D190F1F] PRIMARY KEY ([id]),
        CONSTRAINT [FK__Items__biddingNo__36B12243] FOREIGN KEY ([biddingNo]) REFERENCES [Biddings] ([biddingNo]),
        CONSTRAINT [FK__Items__userID__35BCFE0A] FOREIGN KEY ([userID]) REFERENCES [Users] ([ID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [EmployeeItem] (
        [empID] int NOT NULL,
        [itemID] int NOT NULL,
        [doctor] varchar(50) NULL,
        CONSTRAINT [PK__Employee__1AD9FEE9A85FDFFB] PRIMARY KEY ([empID], [itemID]),
        CONSTRAINT [FK__EmployeeI__empID__3F466844] FOREIGN KEY ([empID]) REFERENCES [Employees] ([empID]),
        CONSTRAINT [FK__EmployeeI__itemI__403A8C7D] FOREIGN KEY ([itemID]) REFERENCES [Items] ([id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE TABLE [Invoice] (
        [invoiceID] int NOT NULL IDENTITY,
        [userID] int NULL,
        [invoiceNo] int NULL,
        [type] varchar(20) NULL,
        [itemID] int NULL,
        [dateTime] datetime NULL,
        [total] decimal(10,2) NULL,
        CONSTRAINT [PK__Invoice__1252410C2EB72C9C] PRIMARY KEY ([invoiceID]),
        CONSTRAINT [FK__Invoice__itemID__3A81B327] FOREIGN KEY ([itemID]) REFERENCES [Items] ([id]),
        CONSTRAINT [FK__Invoice__userID__398D8EEE] FOREIGN KEY ([userID]) REFERENCES [Users] ([ID])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_About_empID] ON [About] ([empID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_Admin_titleID] ON [Admin] ([titleID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UQ__Admin__AFB3EC6C0CBA2A3A] ON [Admin] ([empID]) WHERE [empID] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_Banners_empID] ON [Banners] ([empID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_Biddings_userID] ON [Biddings] ([userID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_EmployeeItem_itemID] ON [EmployeeItem] ([itemID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_Invoice_itemID] ON [Invoice] ([itemID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_Invoice_userID] ON [Invoice] ([userID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_Items_biddingNo] ON [Items] ([biddingNo]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_Items_userID] ON [Items] ([userID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_Mails_userID] ON [Mails] ([userID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_PriceAndRate_empID] ON [PriceAndRate] ([empID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_PrivacyAndPolicy_empID] ON [PrivacyAndPolicy] ([empID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_Salaries_empID] ON [Salaries] ([empID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_SocialMedia_empID] ON [SocialMedia] ([empID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_TermsAndConditions_empID] ON [TermsAndConditions] ([empID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    CREATE INDEX [IX_Titles_empID] ON [Titles] ([empID]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240207095324_AddDatabase'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240207095324_AddDatabase', N'8.0.1');
END;
GO

COMMIT;
GO

