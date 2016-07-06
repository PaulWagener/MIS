CREATE TABLE [dbo].[Werkvorm] (
    [Type]         VARCHAR (5)  NOT NULL,
    [Omschrijving] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Werkvorm] PRIMARY KEY CLUSTERED ([Type] ASC)
);

