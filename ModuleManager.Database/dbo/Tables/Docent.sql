CREATE TABLE [dbo].[Docent] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Naam] NVARCHAR (MAX) NOT NULL,
    [LinkedUser] VARCHAR(50) NULL, 
    CONSTRAINT [PK_Docenten] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LinkedUser_User] FOREIGN KEY ([LinkedUser]) REFERENCES [dbo].[User] ([UserNaam]),
);

