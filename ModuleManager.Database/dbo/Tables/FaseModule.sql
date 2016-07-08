﻿CREATE TABLE [dbo].[FaseModule] (
    [FaseNaam]         VARCHAR (50) NOT NULL,
    [ModuleCursusCode] VARCHAR (50) NOT NULL,
    [ModuleSchooljaar] VARCHAR (8)  NOT NULL,
    CONSTRAINT [FK_FaseModules_Fase] FOREIGN KEY ([FaseNaam]) REFERENCES [dbo].[Fase] ([Naam]),
    CONSTRAINT [FK_FaseModules_Module] FOREIGN KEY ([ModuleCursusCode], [ModuleSchooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar]), 
    CONSTRAINT [PK_FaseModule] PRIMARY KEY ([FaseNaam], [ModuleSchooljaar], [ModuleCursusCode])
);
