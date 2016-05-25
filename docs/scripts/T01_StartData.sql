/** Wat start data om je applicatie mee te beginnen **/
/** Door: Stijn Smulders **/
/** Sprint: Dev ready **/
/** Datum: 12/15/2015 **/
use [Studiegids.Domain.Dev]

INSERT INTO SCHOOLJAAR (JaarId) values ('1416'), ('1516')
INSERT INTO Blok (BlokId) values (1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12),(13),(14),(15),(16)
INSERT INTO FaseType  values ('Major'),('Minor'),('Properdeuse')
INSERT INTO Opleiding values ('Informatica','1516','Informatica')
INSERT INTO Fase values ('SO', '1516', 'Software ontwikkeling', 'Major', 'Informatica', '1516')
INSERT INTO Fase values ('BI', '1516', 'Bedrijfskundige Informatica', 'Major', 'Informatica', '1516')
INSERT INTO Niveau values ('Beginner') , ('Expert'), ('Gevorderde')
INSERT INTO Onderdeel values ('ABV1') , ('ABV2'), ('ABV3'), ('ABV4')
INSERT INTO Status values ('Compleet (gecontroleerd)') , ('Compleet (ongecontroleerd)'), ('Incompleet'), ('Nieuw')
INSERT INTO Leerlijn values ('Programmeren', '1516'),  ('Testen', '1516'),  ('Architectuur', '1516')
INSERT INTO Tag values ('MVC' ), ('MVVM' ), ('Observer' )
INSERT INTO Werkvorm values ('PR', 'Practicum'), ('TH', 'Hoorcollege'), ('WS', 'Workshop')
INSERT INTO Competentie values ( 'BC1',	'1516',	'Procesanalyse uitvoeren', 'Stelt de student in staat.. ' )
INSERT INTO Competentie values ( 'BC2',	'1516',	'Testplan opzetten', 'Stelt de student in staat.. ' )
INSERT INTO Competentie values ( 'BC3',	'1516',	'Technisch ontwerpen', 'Stelt de student in staat.. ' )
INSERT INTO Docenten values ('Bart Mutsaers'), ('Ger Saris'), ('Stijn Smulders')
INSERT INTO [Icons] values ('database'),( 'code')

use [UserDal]
INSERT INTO [dbo].[SysteemRol] values ('Admin'), ('Docent')
INSERT INTO [dbo].[User] values ('admin', 'D033E22AE348AEB5660FC2140AEC35850C4DA997', 'Admin' , 'admin@test.nl', 'A. Dmin', 0)
