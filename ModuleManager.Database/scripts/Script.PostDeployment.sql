/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

go
:r .\testData\Schooljaar.sql
:r .\testData\Blok.sql
:r .\testData\FaseType.sql
:r .\testData\Opleiding.sql
:r .\testData\Fase.sql
:r .\testData\Niveau.sql
:r .\testData\Onderdeel.sql
:r .\testData\Status.sql
:r .\testData\Leerlijn.sql
:r .\testData\Tag.sql
:r .\testData\Werkvorm.sql
:r .\testData\Competentie.sql
:r .\testData\Docent.sql
:r .\testData\Icon.sql
:r .\testData\Module.sql
:r .\testData\SysteemRol.sql

--:r .\storedProcedures\stored_procedures.sql