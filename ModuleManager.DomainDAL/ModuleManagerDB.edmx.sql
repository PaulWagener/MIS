
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/24/2015 14:10:40
-- Generated from EDMX file: C:\TFS\studiegids\ModuleManager.DomainDAL\ModuleManagerDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DomainDal.Test];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Beoordelingen_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Beoordelingen] DROP CONSTRAINT [FK_Beoordelingen_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_FaseModules_Blok]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FaseModules] DROP CONSTRAINT [FK_FaseModules_Blok];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleCompetentie_Competentie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleCompetentie] DROP CONSTRAINT [FK_ModuleCompetentie_Competentie];
GO
IF OBJECT_ID(N'[dbo].[FK_Docent_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Docent] DROP CONSTRAINT [FK_Docent_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_Fase_FaseType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Fase] DROP CONSTRAINT [FK_Fase_FaseType];
GO
IF OBJECT_ID(N'[dbo].[FK_Fase_Opleiding]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Fase] DROP CONSTRAINT [FK_Fase_Opleiding];
GO
IF OBJECT_ID(N'[dbo].[FK_FaseModules_Fase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FaseModules] DROP CONSTRAINT [FK_FaseModules_Fase];
GO
IF OBJECT_ID(N'[dbo].[FK_FaseModules_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FaseModules] DROP CONSTRAINT [FK_FaseModules_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_Module_Icons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Module] DROP CONSTRAINT [FK_Module_Icons];
GO
IF OBJECT_ID(N'[dbo].[FK_Leerdoelen_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Leerdoelen] DROP CONSTRAINT [FK_Leerdoelen_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_Leermiddelen_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Leermiddelen] DROP CONSTRAINT [FK_Leermiddelen_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_Module_Onderdeel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Module] DROP CONSTRAINT [FK_Module_Onderdeel];
GO
IF OBJECT_ID(N'[dbo].[FK_Module_Status]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Module] DROP CONSTRAINT [FK_Module_Status];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleCompetentie_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleCompetentie] DROP CONSTRAINT [FK_ModuleCompetentie_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleWerkvorm_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleWerkvorm] DROP CONSTRAINT [FK_ModuleWerkvorm_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_StudieBelasting_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudieBelasting] DROP CONSTRAINT [FK_StudieBelasting_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_StudiePunten_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudiePunten] DROP CONSTRAINT [FK_StudiePunten_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_Weekplanning_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Weekplanning] DROP CONSTRAINT [FK_Weekplanning_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleCompetentie_Niveau]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleCompetentie] DROP CONSTRAINT [FK_ModuleCompetentie_Niveau];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleWerkvorm_Werkvorm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleWerkvorm] DROP CONSTRAINT [FK_ModuleWerkvorm_Werkvorm];
GO
IF OBJECT_ID(N'[dbo].[FK_Opleiding_Schooljaar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Opleiding] DROP CONSTRAINT [FK_Opleiding_Schooljaar];
GO
IF OBJECT_ID(N'[dbo].[FK_StudiePunten_Toetsvorm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudiePunten] DROP CONSTRAINT [FK_StudiePunten_Toetsvorm];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleLeerlijn_Leerlijn]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleLeerlijn] DROP CONSTRAINT [FK_ModuleLeerlijn_Leerlijn];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleLeerlijn_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleLeerlijn] DROP CONSTRAINT [FK_ModuleLeerlijn_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleTag_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleTag] DROP CONSTRAINT [FK_ModuleTag_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_ModuleTag_Tag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModuleTag] DROP CONSTRAINT [FK_ModuleTag_Tag];
GO
IF OBJECT_ID(N'[dbo].[FK_Voorkennis_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Voorkennis] DROP CONSTRAINT [FK_Voorkennis_Module];
GO
IF OBJECT_ID(N'[dbo].[FK_Voorkennis_Module1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Voorkennis] DROP CONSTRAINT [FK_Voorkennis_Module1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Beoordelingen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Beoordelingen];
GO
IF OBJECT_ID(N'[dbo].[Blok]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Blok];
GO
IF OBJECT_ID(N'[dbo].[Competentie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Competentie];
GO
IF OBJECT_ID(N'[dbo].[Docent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Docent];
GO
IF OBJECT_ID(N'[dbo].[Fase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fase];
GO
IF OBJECT_ID(N'[dbo].[FaseModules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FaseModules];
GO
IF OBJECT_ID(N'[dbo].[FaseType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FaseType];
GO
IF OBJECT_ID(N'[dbo].[Icons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Icons];
GO
IF OBJECT_ID(N'[dbo].[Leerdoelen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Leerdoelen];
GO
IF OBJECT_ID(N'[dbo].[Leerlijn]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Leerlijn];
GO
IF OBJECT_ID(N'[dbo].[Leermiddelen]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Leermiddelen];
GO
IF OBJECT_ID(N'[dbo].[Module]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Module];
GO
IF OBJECT_ID(N'[dbo].[ModuleCompetentie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModuleCompetentie];
GO
IF OBJECT_ID(N'[dbo].[ModuleWerkvorm]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModuleWerkvorm];
GO
IF OBJECT_ID(N'[dbo].[Niveau]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Niveau];
GO
IF OBJECT_ID(N'[dbo].[Onderdeel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Onderdeel];
GO
IF OBJECT_ID(N'[dbo].[Opleiding]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Opleiding];
GO
IF OBJECT_ID(N'[dbo].[Schooljaar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Schooljaar];
GO
IF OBJECT_ID(N'[dbo].[Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Status];
GO
IF OBJECT_ID(N'[dbo].[StudieBelasting]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudieBelasting];
GO
IF OBJECT_ID(N'[dbo].[StudiePunten]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudiePunten];
GO
IF OBJECT_ID(N'[dbo].[Tag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tag];
GO
IF OBJECT_ID(N'[dbo].[Toetsvorm]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Toetsvorm];
GO
IF OBJECT_ID(N'[dbo].[Weekplanning]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Weekplanning];
GO
IF OBJECT_ID(N'[dbo].[Werkvorm]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Werkvorm];
GO
IF OBJECT_ID(N'[dbo].[ModuleLeerlijn]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModuleLeerlijn];
GO
IF OBJECT_ID(N'[dbo].[ModuleTag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModuleTag];
GO
IF OBJECT_ID(N'[dbo].[Voorkennis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Voorkennis];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Beoordelingen'
CREATE TABLE [dbo].[Beoordelingen] (
    [CursusCode] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Beschrijving] varchar(500)  NOT NULL
);
GO

-- Creating table 'Blok'
CREATE TABLE [dbo].[Blok] (
    [BlokId] varchar(5)  NOT NULL
);
GO

-- Creating table 'Competentie'
CREATE TABLE [dbo].[Competentie] (
    [Code] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [Naam] varchar(50)  NOT NULL,
    [Beschrijving] varchar(max)  NOT NULL
);
GO

-- Creating table 'Docent'
CREATE TABLE [dbo].[Docent] (
    [CursusCode] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL
);
GO

-- Creating table 'Fase'
CREATE TABLE [dbo].[Fase] (
    [Naam] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [Beschrijving] varchar(500)  NULL,
    [FaseType] varchar(50)  NOT NULL,
    [OpleidingNaam] varchar(50)  NOT NULL,
    [OpleidingSchooljaar] varchar(8)  NOT NULL
);
GO

-- Creating table 'FaseModules'
CREATE TABLE [dbo].[FaseModules] (
    [FaseNaam] varchar(50)  NOT NULL,
    [FaseSchooljaar] varchar(8)  NOT NULL,
    [ModuleCursusCode] varchar(50)  NOT NULL,
    [ModuleSchooljaar] varchar(8)  NOT NULL,
    [Blok] varchar(5)  NOT NULL,
    [OpleidingNaam] varchar(50)  NOT NULL,
    [OpleidingSchooljaar] varchar(8)  NOT NULL
);
GO

-- Creating table 'FaseType'
CREATE TABLE [dbo].[FaseType] (
    [Type] varchar(50)  NOT NULL
);
GO

-- Creating table 'Icons'
CREATE TABLE [dbo].[Icons] (
    [Icon] char(20)  NOT NULL
);
GO

-- Creating table 'Leerdoelen'
CREATE TABLE [dbo].[Leerdoelen] (
    [CursusCode] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Beschrijving] varchar(500)  NOT NULL
);
GO

-- Creating table 'Leerlijn'
CREATE TABLE [dbo].[Leerlijn] (
    [Naam] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL
);
GO

-- Creating table 'Leermiddelen'
CREATE TABLE [dbo].[Leermiddelen] (
    [CursusCode] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Beschrijving] varchar(500)  NOT NULL
);
GO

-- Creating table 'Module'
CREATE TABLE [dbo].[Module] (
    [CursusCode] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [Beschrijving] varchar(max)  NULL,
    [Naam] varchar(50)  NOT NULL,
    [Verantwoordelijke] varchar(50)  NOT NULL,
    [Status] varchar(50)  NOT NULL,
    [OnderdeelCode] varchar(50)  NOT NULL,
    [Icon] char(20)  NOT NULL
);
GO

-- Creating table 'ModuleCompetentie'
CREATE TABLE [dbo].[ModuleCompetentie] (
    [CursusCode] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [CompetentieCode] varchar(50)  NOT NULL,
    [CompetentieSchooljaar] varchar(8)  NOT NULL,
    [Niveau] varchar(50)  NOT NULL
);
GO

-- Creating table 'ModuleWerkvorm'
CREATE TABLE [dbo].[ModuleWerkvorm] (
    [CursusCode] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [WerkvormType] varchar(5)  NOT NULL,
    [Organisatie] varchar(max)  NULL
);
GO

-- Creating table 'Niveau'
CREATE TABLE [dbo].[Niveau] (
    [Niveau1] varchar(50)  NOT NULL
);
GO

-- Creating table 'Onderdeel'
CREATE TABLE [dbo].[Onderdeel] (
    [Code] varchar(50)  NOT NULL
);
GO

-- Creating table 'Opleiding'
CREATE TABLE [dbo].[Opleiding] (
    [Naam] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [Beschrijving] varchar(500)  NULL
);
GO

-- Creating table 'Schooljaar'
CREATE TABLE [dbo].[Schooljaar] (
    [JaarId] varchar(8)  NOT NULL
);
GO

-- Creating table 'Status'
CREATE TABLE [dbo].[Status] (
    [Status1] varchar(50)  NOT NULL
);
GO

-- Creating table 'StudieBelasting'
CREATE TABLE [dbo].[StudieBelasting] (
    [CursusCode] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [Activiteit] varchar(50)  NOT NULL,
    [ContactUren] int  NULL,
    [Duur] varchar(50)  NOT NULL,
    [Frequentie] varchar(50)  NOT NULL,
    [SBU] int  NOT NULL
);
GO

-- Creating table 'StudiePunten'
CREATE TABLE [dbo].[StudiePunten] (
    [CursusCode] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [ToetsCode] varchar(50)  NOT NULL,
    [Toetsvorm] varchar(50)  NULL,
    [EC] decimal(18,0)  NOT NULL,
    [Minimum] varchar(50)  NULL
);
GO

-- Creating table 'Tag'
CREATE TABLE [dbo].[Tag] (
    [Naam] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL
);
GO

-- Creating table 'Toetsvorm'
CREATE TABLE [dbo].[Toetsvorm] (
    [Type] varchar(50)  NOT NULL
);
GO

-- Creating table 'Weekplanning'
CREATE TABLE [dbo].[Weekplanning] (
    [CursusCode] varchar(50)  NOT NULL,
    [Schooljaar] varchar(8)  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Week] varchar(10)  NOT NULL,
    [Onderwerp] varchar(500)  NOT NULL
);
GO

-- Creating table 'Werkvorm'
CREATE TABLE [dbo].[Werkvorm] (
    [Type] varchar(5)  NOT NULL,
    [Omschrijving] varchar(50)  NOT NULL
);
GO

-- Creating table 'ModuleLeerlijn'
CREATE TABLE [dbo].[ModuleLeerlijn] (
    [Leerlijn_Naam] varchar(50)  NOT NULL,
    [Leerlijn_Schooljaar] varchar(8)  NOT NULL,
    [Module_CursusCode] varchar(50)  NOT NULL,
    [Module_Schooljaar] varchar(8)  NOT NULL
);
GO

-- Creating table 'ModuleTag'
CREATE TABLE [dbo].[ModuleTag] (
    [Module_CursusCode] varchar(50)  NOT NULL,
    [Module_Schooljaar] varchar(8)  NOT NULL,
    [Tag_Naam] varchar(50)  NOT NULL,
    [Tag_Schooljaar] varchar(8)  NOT NULL
);
GO

-- Creating table 'Voorkennis'
CREATE TABLE [dbo].[Voorkennis] (
    [Module2_CursusCode] varchar(50)  NOT NULL,
    [Module2_Schooljaar] varchar(8)  NOT NULL,
    [Module1_CursusCode] varchar(50)  NOT NULL,
    [Module1_Schooljaar] varchar(8)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CursusCode], [Schooljaar], [Id] in table 'Beoordelingen'
ALTER TABLE [dbo].[Beoordelingen]
ADD CONSTRAINT [PK_Beoordelingen]
    PRIMARY KEY CLUSTERED ([CursusCode], [Schooljaar], [Id] ASC);
GO

-- Creating primary key on [BlokId] in table 'Blok'
ALTER TABLE [dbo].[Blok]
ADD CONSTRAINT [PK_Blok]
    PRIMARY KEY CLUSTERED ([BlokId] ASC);
GO

-- Creating primary key on [Code], [Schooljaar] in table 'Competentie'
ALTER TABLE [dbo].[Competentie]
ADD CONSTRAINT [PK_Competentie]
    PRIMARY KEY CLUSTERED ([Code], [Schooljaar] ASC);
GO

-- Creating primary key on [CursusCode], [Schooljaar], [Id] in table 'Docent'
ALTER TABLE [dbo].[Docent]
ADD CONSTRAINT [PK_Docent]
    PRIMARY KEY CLUSTERED ([CursusCode], [Schooljaar], [Id] ASC);
GO

-- Creating primary key on [Naam], [Schooljaar], [OpleidingNaam], [OpleidingSchooljaar] in table 'Fase'
ALTER TABLE [dbo].[Fase]
ADD CONSTRAINT [PK_Fase]
    PRIMARY KEY CLUSTERED ([Naam], [Schooljaar], [OpleidingNaam], [OpleidingSchooljaar] ASC);
GO

-- Creating primary key on [FaseNaam], [FaseSchooljaar], [ModuleCursusCode], [ModuleSchooljaar], [OpleidingNaam], [OpleidingSchooljaar] in table 'FaseModules'
ALTER TABLE [dbo].[FaseModules]
ADD CONSTRAINT [PK_FaseModules]
    PRIMARY KEY CLUSTERED ([FaseNaam], [FaseSchooljaar], [ModuleCursusCode], [ModuleSchooljaar], [OpleidingNaam], [OpleidingSchooljaar] ASC);
GO

-- Creating primary key on [Type] in table 'FaseType'
ALTER TABLE [dbo].[FaseType]
ADD CONSTRAINT [PK_FaseType]
    PRIMARY KEY CLUSTERED ([Type] ASC);
GO

-- Creating primary key on [Icon] in table 'Icons'
ALTER TABLE [dbo].[Icons]
ADD CONSTRAINT [PK_Icons]
    PRIMARY KEY CLUSTERED ([Icon] ASC);
GO

-- Creating primary key on [CursusCode], [Schooljaar], [Id] in table 'Leerdoelen'
ALTER TABLE [dbo].[Leerdoelen]
ADD CONSTRAINT [PK_Leerdoelen]
    PRIMARY KEY CLUSTERED ([CursusCode], [Schooljaar], [Id] ASC);
GO

-- Creating primary key on [Naam], [Schooljaar] in table 'Leerlijn'
ALTER TABLE [dbo].[Leerlijn]
ADD CONSTRAINT [PK_Leerlijn]
    PRIMARY KEY CLUSTERED ([Naam], [Schooljaar] ASC);
GO

-- Creating primary key on [CursusCode], [Schooljaar], [Id] in table 'Leermiddelen'
ALTER TABLE [dbo].[Leermiddelen]
ADD CONSTRAINT [PK_Leermiddelen]
    PRIMARY KEY CLUSTERED ([CursusCode], [Schooljaar], [Id] ASC);
GO

-- Creating primary key on [CursusCode], [Schooljaar] in table 'Module'
ALTER TABLE [dbo].[Module]
ADD CONSTRAINT [PK_Module]
    PRIMARY KEY CLUSTERED ([CursusCode], [Schooljaar] ASC);
GO

-- Creating primary key on [CursusCode], [Schooljaar], [CompetentieCode], [CompetentieSchooljaar] in table 'ModuleCompetentie'
ALTER TABLE [dbo].[ModuleCompetentie]
ADD CONSTRAINT [PK_ModuleCompetentie]
    PRIMARY KEY CLUSTERED ([CursusCode], [Schooljaar], [CompetentieCode], [CompetentieSchooljaar] ASC);
GO

-- Creating primary key on [CursusCode], [Schooljaar], [WerkvormType] in table 'ModuleWerkvorm'
ALTER TABLE [dbo].[ModuleWerkvorm]
ADD CONSTRAINT [PK_ModuleWerkvorm]
    PRIMARY KEY CLUSTERED ([CursusCode], [Schooljaar], [WerkvormType] ASC);
GO

-- Creating primary key on [Niveau1] in table 'Niveau'
ALTER TABLE [dbo].[Niveau]
ADD CONSTRAINT [PK_Niveau]
    PRIMARY KEY CLUSTERED ([Niveau1] ASC);
GO

-- Creating primary key on [Code] in table 'Onderdeel'
ALTER TABLE [dbo].[Onderdeel]
ADD CONSTRAINT [PK_Onderdeel]
    PRIMARY KEY CLUSTERED ([Code] ASC);
GO

-- Creating primary key on [Naam], [Schooljaar] in table 'Opleiding'
ALTER TABLE [dbo].[Opleiding]
ADD CONSTRAINT [PK_Opleiding]
    PRIMARY KEY CLUSTERED ([Naam], [Schooljaar] ASC);
GO

-- Creating primary key on [JaarId] in table 'Schooljaar'
ALTER TABLE [dbo].[Schooljaar]
ADD CONSTRAINT [PK_Schooljaar]
    PRIMARY KEY CLUSTERED ([JaarId] ASC);
GO

-- Creating primary key on [Status1] in table 'Status'
ALTER TABLE [dbo].[Status]
ADD CONSTRAINT [PK_Status]
    PRIMARY KEY CLUSTERED ([Status1] ASC);
GO

-- Creating primary key on [CursusCode], [Schooljaar], [Activiteit] in table 'StudieBelasting'
ALTER TABLE [dbo].[StudieBelasting]
ADD CONSTRAINT [PK_StudieBelasting]
    PRIMARY KEY CLUSTERED ([CursusCode], [Schooljaar], [Activiteit] ASC);
GO

-- Creating primary key on [CursusCode], [Schooljaar], [ToetsCode] in table 'StudiePunten'
ALTER TABLE [dbo].[StudiePunten]
ADD CONSTRAINT [PK_StudiePunten]
    PRIMARY KEY CLUSTERED ([CursusCode], [Schooljaar], [ToetsCode] ASC);
GO

-- Creating primary key on [Naam], [Schooljaar] in table 'Tag'
ALTER TABLE [dbo].[Tag]
ADD CONSTRAINT [PK_Tag]
    PRIMARY KEY CLUSTERED ([Naam], [Schooljaar] ASC);
GO

-- Creating primary key on [Type] in table 'Toetsvorm'
ALTER TABLE [dbo].[Toetsvorm]
ADD CONSTRAINT [PK_Toetsvorm]
    PRIMARY KEY CLUSTERED ([Type] ASC);
GO

-- Creating primary key on [CursusCode], [Schooljaar], [Id] in table 'Weekplanning'
ALTER TABLE [dbo].[Weekplanning]
ADD CONSTRAINT [PK_Weekplanning]
    PRIMARY KEY CLUSTERED ([CursusCode], [Schooljaar], [Id] ASC);
GO

-- Creating primary key on [Type] in table 'Werkvorm'
ALTER TABLE [dbo].[Werkvorm]
ADD CONSTRAINT [PK_Werkvorm]
    PRIMARY KEY CLUSTERED ([Type] ASC);
GO

-- Creating primary key on [Leerlijn_Naam], [Leerlijn_Schooljaar], [Module_CursusCode], [Module_Schooljaar] in table 'ModuleLeerlijn'
ALTER TABLE [dbo].[ModuleLeerlijn]
ADD CONSTRAINT [PK_ModuleLeerlijn]
    PRIMARY KEY CLUSTERED ([Leerlijn_Naam], [Leerlijn_Schooljaar], [Module_CursusCode], [Module_Schooljaar] ASC);
GO

-- Creating primary key on [Module_CursusCode], [Module_Schooljaar], [Tag_Naam], [Tag_Schooljaar] in table 'ModuleTag'
ALTER TABLE [dbo].[ModuleTag]
ADD CONSTRAINT [PK_ModuleTag]
    PRIMARY KEY CLUSTERED ([Module_CursusCode], [Module_Schooljaar], [Tag_Naam], [Tag_Schooljaar] ASC);
GO

-- Creating primary key on [Module2_CursusCode], [Module2_Schooljaar], [Module1_CursusCode], [Module1_Schooljaar] in table 'Voorkennis'
ALTER TABLE [dbo].[Voorkennis]
ADD CONSTRAINT [PK_Voorkennis]
    PRIMARY KEY CLUSTERED ([Module2_CursusCode], [Module2_Schooljaar], [Module1_CursusCode], [Module1_Schooljaar] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CursusCode], [Schooljaar] in table 'Beoordelingen'
ALTER TABLE [dbo].[Beoordelingen]
ADD CONSTRAINT [FK_Beoordelingen_Module]
    FOREIGN KEY ([CursusCode], [Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Blok] in table 'FaseModules'
ALTER TABLE [dbo].[FaseModules]
ADD CONSTRAINT [FK_FaseModules_Blok]
    FOREIGN KEY ([Blok])
    REFERENCES [dbo].[Blok]
        ([BlokId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FaseModules_Blok'
CREATE INDEX [IX_FK_FaseModules_Blok]
ON [dbo].[FaseModules]
    ([Blok]);
GO

-- Creating foreign key on [CompetentieCode], [CompetentieSchooljaar] in table 'ModuleCompetentie'
ALTER TABLE [dbo].[ModuleCompetentie]
ADD CONSTRAINT [FK_ModuleCompetentie_Competentie]
    FOREIGN KEY ([CompetentieCode], [CompetentieSchooljaar])
    REFERENCES [dbo].[Competentie]
        ([Code], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModuleCompetentie_Competentie'
CREATE INDEX [IX_FK_ModuleCompetentie_Competentie]
ON [dbo].[ModuleCompetentie]
    ([CompetentieCode], [CompetentieSchooljaar]);
GO

-- Creating foreign key on [CursusCode], [Schooljaar] in table 'Docent'
ALTER TABLE [dbo].[Docent]
ADD CONSTRAINT [FK_Docent_Module]
    FOREIGN KEY ([CursusCode], [Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FaseType] in table 'Fase'
ALTER TABLE [dbo].[Fase]
ADD CONSTRAINT [FK_Fase_FaseType]
    FOREIGN KEY ([FaseType])
    REFERENCES [dbo].[FaseType]
        ([Type])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Fase_FaseType'
CREATE INDEX [IX_FK_Fase_FaseType]
ON [dbo].[Fase]
    ([FaseType]);
GO

-- Creating foreign key on [OpleidingNaam], [OpleidingSchooljaar] in table 'Fase'
ALTER TABLE [dbo].[Fase]
ADD CONSTRAINT [FK_Fase_Opleiding]
    FOREIGN KEY ([OpleidingNaam], [OpleidingSchooljaar])
    REFERENCES [dbo].[Opleiding]
        ([Naam], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Fase_Opleiding'
CREATE INDEX [IX_FK_Fase_Opleiding]
ON [dbo].[Fase]
    ([OpleidingNaam], [OpleidingSchooljaar]);
GO

-- Creating foreign key on [FaseNaam], [FaseSchooljaar], [OpleidingNaam], [OpleidingSchooljaar] in table 'FaseModules'
ALTER TABLE [dbo].[FaseModules]
ADD CONSTRAINT [FK_FaseModules_Fase]
    FOREIGN KEY ([FaseNaam], [FaseSchooljaar], [OpleidingNaam], [OpleidingSchooljaar])
    REFERENCES [dbo].[Fase]
        ([Naam], [Schooljaar], [OpleidingNaam], [OpleidingSchooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FaseModules_Fase'
CREATE INDEX [IX_FK_FaseModules_Fase]
ON [dbo].[FaseModules]
    ([FaseNaam], [FaseSchooljaar], [OpleidingNaam], [OpleidingSchooljaar]);
GO

-- Creating foreign key on [ModuleCursusCode], [ModuleSchooljaar] in table 'FaseModules'
ALTER TABLE [dbo].[FaseModules]
ADD CONSTRAINT [FK_FaseModules_Module]
    FOREIGN KEY ([ModuleCursusCode], [ModuleSchooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FaseModules_Module'
CREATE INDEX [IX_FK_FaseModules_Module]
ON [dbo].[FaseModules]
    ([ModuleCursusCode], [ModuleSchooljaar]);
GO

-- Creating foreign key on [Icon] in table 'Module'
ALTER TABLE [dbo].[Module]
ADD CONSTRAINT [FK_Module_Icons]
    FOREIGN KEY ([Icon])
    REFERENCES [dbo].[Icons]
        ([Icon])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Module_Icons'
CREATE INDEX [IX_FK_Module_Icons]
ON [dbo].[Module]
    ([Icon]);
GO

-- Creating foreign key on [CursusCode], [Schooljaar] in table 'Leerdoelen'
ALTER TABLE [dbo].[Leerdoelen]
ADD CONSTRAINT [FK_Leerdoelen_Module]
    FOREIGN KEY ([CursusCode], [Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CursusCode], [Schooljaar] in table 'Leermiddelen'
ALTER TABLE [dbo].[Leermiddelen]
ADD CONSTRAINT [FK_Leermiddelen_Module]
    FOREIGN KEY ([CursusCode], [Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [OnderdeelCode] in table 'Module'
ALTER TABLE [dbo].[Module]
ADD CONSTRAINT [FK_Module_Onderdeel]
    FOREIGN KEY ([OnderdeelCode])
    REFERENCES [dbo].[Onderdeel]
        ([Code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Module_Onderdeel'
CREATE INDEX [IX_FK_Module_Onderdeel]
ON [dbo].[Module]
    ([OnderdeelCode]);
GO

-- Creating foreign key on [Status] in table 'Module'
ALTER TABLE [dbo].[Module]
ADD CONSTRAINT [FK_Module_Status]
    FOREIGN KEY ([Status])
    REFERENCES [dbo].[Status]
        ([Status1])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Module_Status'
CREATE INDEX [IX_FK_Module_Status]
ON [dbo].[Module]
    ([Status]);
GO

-- Creating foreign key on [CursusCode], [Schooljaar] in table 'ModuleCompetentie'
ALTER TABLE [dbo].[ModuleCompetentie]
ADD CONSTRAINT [FK_ModuleCompetentie_Module]
    FOREIGN KEY ([CursusCode], [Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CursusCode], [Schooljaar] in table 'ModuleWerkvorm'
ALTER TABLE [dbo].[ModuleWerkvorm]
ADD CONSTRAINT [FK_ModuleWerkvorm_Module]
    FOREIGN KEY ([CursusCode], [Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CursusCode], [Schooljaar] in table 'StudieBelasting'
ALTER TABLE [dbo].[StudieBelasting]
ADD CONSTRAINT [FK_StudieBelasting_Module]
    FOREIGN KEY ([CursusCode], [Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CursusCode], [Schooljaar] in table 'StudiePunten'
ALTER TABLE [dbo].[StudiePunten]
ADD CONSTRAINT [FK_StudiePunten_Module]
    FOREIGN KEY ([CursusCode], [Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CursusCode], [Schooljaar] in table 'Weekplanning'
ALTER TABLE [dbo].[Weekplanning]
ADD CONSTRAINT [FK_Weekplanning_Module]
    FOREIGN KEY ([CursusCode], [Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Niveau] in table 'ModuleCompetentie'
ALTER TABLE [dbo].[ModuleCompetentie]
ADD CONSTRAINT [FK_ModuleCompetentie_Niveau]
    FOREIGN KEY ([Niveau])
    REFERENCES [dbo].[Niveau]
        ([Niveau1])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModuleCompetentie_Niveau'
CREATE INDEX [IX_FK_ModuleCompetentie_Niveau]
ON [dbo].[ModuleCompetentie]
    ([Niveau]);
GO

-- Creating foreign key on [WerkvormType] in table 'ModuleWerkvorm'
ALTER TABLE [dbo].[ModuleWerkvorm]
ADD CONSTRAINT [FK_ModuleWerkvorm_Werkvorm]
    FOREIGN KEY ([WerkvormType])
    REFERENCES [dbo].[Werkvorm]
        ([Type])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModuleWerkvorm_Werkvorm'
CREATE INDEX [IX_FK_ModuleWerkvorm_Werkvorm]
ON [dbo].[ModuleWerkvorm]
    ([WerkvormType]);
GO

-- Creating foreign key on [Schooljaar] in table 'Opleiding'
ALTER TABLE [dbo].[Opleiding]
ADD CONSTRAINT [FK_Opleiding_Schooljaar]
    FOREIGN KEY ([Schooljaar])
    REFERENCES [dbo].[Schooljaar]
        ([JaarId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Opleiding_Schooljaar'
CREATE INDEX [IX_FK_Opleiding_Schooljaar]
ON [dbo].[Opleiding]
    ([Schooljaar]);
GO

-- Creating foreign key on [Toetsvorm] in table 'StudiePunten'
ALTER TABLE [dbo].[StudiePunten]
ADD CONSTRAINT [FK_StudiePunten_Toetsvorm]
    FOREIGN KEY ([Toetsvorm])
    REFERENCES [dbo].[Toetsvorm]
        ([Type])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudiePunten_Toetsvorm'
CREATE INDEX [IX_FK_StudiePunten_Toetsvorm]
ON [dbo].[StudiePunten]
    ([Toetsvorm]);
GO

-- Creating foreign key on [Leerlijn_Naam], [Leerlijn_Schooljaar] in table 'ModuleLeerlijn'
ALTER TABLE [dbo].[ModuleLeerlijn]
ADD CONSTRAINT [FK_ModuleLeerlijn_Leerlijn]
    FOREIGN KEY ([Leerlijn_Naam], [Leerlijn_Schooljaar])
    REFERENCES [dbo].[Leerlijn]
        ([Naam], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Module_CursusCode], [Module_Schooljaar] in table 'ModuleLeerlijn'
ALTER TABLE [dbo].[ModuleLeerlijn]
ADD CONSTRAINT [FK_ModuleLeerlijn_Module]
    FOREIGN KEY ([Module_CursusCode], [Module_Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModuleLeerlijn_Module'
CREATE INDEX [IX_FK_ModuleLeerlijn_Module]
ON [dbo].[ModuleLeerlijn]
    ([Module_CursusCode], [Module_Schooljaar]);
GO

-- Creating foreign key on [Module_CursusCode], [Module_Schooljaar] in table 'ModuleTag'
ALTER TABLE [dbo].[ModuleTag]
ADD CONSTRAINT [FK_ModuleTag_Module]
    FOREIGN KEY ([Module_CursusCode], [Module_Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tag_Naam], [Tag_Schooljaar] in table 'ModuleTag'
ALTER TABLE [dbo].[ModuleTag]
ADD CONSTRAINT [FK_ModuleTag_Tag]
    FOREIGN KEY ([Tag_Naam], [Tag_Schooljaar])
    REFERENCES [dbo].[Tag]
        ([Naam], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModuleTag_Tag'
CREATE INDEX [IX_FK_ModuleTag_Tag]
ON [dbo].[ModuleTag]
    ([Tag_Naam], [Tag_Schooljaar]);
GO

-- Creating foreign key on [Module2_CursusCode], [Module2_Schooljaar] in table 'Voorkennis'
ALTER TABLE [dbo].[Voorkennis]
ADD CONSTRAINT [FK_Voorkennis_Module]
    FOREIGN KEY ([Module2_CursusCode], [Module2_Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Module1_CursusCode], [Module1_Schooljaar] in table 'Voorkennis'
ALTER TABLE [dbo].[Voorkennis]
ADD CONSTRAINT [FK_Voorkennis_Module1]
    FOREIGN KEY ([Module1_CursusCode], [Module1_Schooljaar])
    REFERENCES [dbo].[Module]
        ([CursusCode], [Schooljaar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Voorkennis_Module1'
CREATE INDEX [IX_FK_Voorkennis_Module1]
ON [dbo].[Voorkennis]
    ([Module1_CursusCode], [Module1_Schooljaar]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------