CREATE TABLE [Competenties].[LeerdoelKwaliteitskenmerk] (
    [LeerdoelId]            INT			 NOT NULL,
    [KwaliteitskenmerkId]   INT			 NOT NULL,
    CONSTRAINT [PK_LeerdoelKwaliteitskenmerk] PRIMARY KEY CLUSTERED ([LeerdoelId], [KwaliteitskenmerkId]),
    CONSTRAINT [FK_LeerdoelKwaliteitskenmerk_Leerdoel] FOREIGN KEY ([LeerdoelId]) REFERENCES [dbo].[Leerdoel] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LeerdoelKwaliteitskenmerk_Kwaliteitskenmerk] FOREIGN KEY ([KwaliteitskenmerkId]) REFERENCES [Competenties].[Kwaliteitskenmerk] ([Id])
);