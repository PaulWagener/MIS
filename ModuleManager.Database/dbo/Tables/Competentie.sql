CREATE TABLE [dbo].[Competentie] (
    [Code]         VARCHAR (50)  NOT NULL,
    [Naam]         VARCHAR (50)  NOT NULL,
    [Beschrijving] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Competentie] PRIMARY KEY CLUSTERED ([Code] ASC)
);

