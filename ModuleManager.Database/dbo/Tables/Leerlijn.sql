CREATE TABLE [dbo].[Leerlijn] (
    [Naam]       VARCHAR (50) NOT NULL,
    [Schooljaar] VARCHAR (8)  NOT NULL,
    CONSTRAINT [PK_Leerlijn] PRIMARY KEY CLUSTERED ([Naam] ASC, [Schooljaar] ASC)
);

