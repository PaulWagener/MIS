CREATE TABLE [dbo].[Opleiding] (
    [Naam]         VARCHAR (50)  NOT NULL,
    [Beschrijving] VARCHAR (500) NULL,
    CONSTRAINT [PK_Opleiding_1] PRIMARY KEY CLUSTERED ([Naam] ASC)
);

