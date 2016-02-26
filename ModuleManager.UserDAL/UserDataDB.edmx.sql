
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/24/2015 14:24:16
-- Generated from EDMX file: C:\TFS\studiegids\ModuleManager.UserDAL\UserDataDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [UserDal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_User_SysteemRol]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_SysteemRol];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[SysteemRol]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SysteemRol];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SysteemRol'
CREATE TABLE [dbo].[SysteemRol] (
    [Role] varchar(50)  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [UserNaam] varchar(50)  NOT NULL,
    [Wachtwoord] varchar(50)  NOT NULL,
    [SysteemRol] varchar(50)  NOT NULL,
    [email] varchar(50)  NULL,
    [naam] varchar(50)  NULL,
    [Blocked] bit  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Role] in table 'SysteemRol'
ALTER TABLE [dbo].[SysteemRol]
ADD CONSTRAINT [PK_SysteemRol]
    PRIMARY KEY CLUSTERED ([Role] ASC);
GO

-- Creating primary key on [UserNaam] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([UserNaam] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SysteemRol] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_User_SysteemRol]
    FOREIGN KEY ([SysteemRol])
    REFERENCES [dbo].[SysteemRol]
        ([Role])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_SysteemRol'
CREATE INDEX [IX_FK_User_SysteemRol]
ON [dbo].[User]
    ([SysteemRol]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------