/* Proces user */
DROP USER [NT AUTHORITY\NETWORK SERVICE]
GO
CREATE USER [NT AUTHORITY\NETWORK SERVICE] FOR LOGIN [NT AUTHORITY\NETWORK SERVICE] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
GRANT EXECUTE TO [NT AUTHORITY\NETWORK SERVICE]
GO

MERGE INTO [User] AS Target  
USING (values 
	('mmaaschu','Admin', 'mmaa.schuurmans@avans.nl', 'Martijn Schuurmans', 0)
) AS Source ([UserNaam], [SysteemRol], [email], [naam], [Blocked])
ON Target.[UserNaam] = Source.[UserNaam]
WHEN NOT MATCHED BY TARGET THEN  
	INSERT ([UserNaam], [SysteemRol], [email], [naam], [Blocked]) VALUES ([UserNaam], [SysteemRol], [email], [naam], [Blocked])
WHEN MATCHED THEN
	UPDATE SET
		[UserNaam] = Source.[UserNaam], 
		[SysteemRol] = Source.[SysteemRol], 
		[email] = Source.[email], 
		[naam] = Source.[naam], 
		[Blocked] = Source.[Blocked];