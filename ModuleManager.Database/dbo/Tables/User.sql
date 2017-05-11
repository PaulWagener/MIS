CREATE TABLE [dbo].[User](
	[UserNaam] [varchar](50) NOT NULL,
	[SysteemRol] [varchar](50) NOT NULL,
	[email] [varchar](50) NULL,
	[naam] [varchar](50) NULL,
	[Blocked] [bit] NOT NULL,
	[Image] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_User_systeemRol] FOREIGN KEY ([SysteemRol]) REFERENCES [dbo].[SysteemRol], 
    CONSTRAINT [PK_User] PRIMARY KEY ([UserNaam])

)
