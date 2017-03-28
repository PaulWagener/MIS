SET IDENTITY_INSERT Competenties.CompetentieOnderdeel ON

MERGE INTO Competenties.CompetentieOnderdeel AS Target  
USING (VALUES 
	(1, 1, 1, 'Aanpak'),
	(2, 1, 2, 'Tijdsbewaking'),
	(3, 1, 3, 'ICT Service management'),
	(4, 1, 4, 'Implementatie'),
	(5, 1, 5, 'IT Governance'),
	(6, 1, 6, 'Ontwikkelmethodiek'),
	(7, 1, 7, 'Ontwikkelomgeving'),
	(8, 1, 8, 'Runtime-omgeving'),
	(9, 1, 9, 'Versiebeheer'),
	(10, 2, 0, 'Algemeen'),
	(11, 2, 1, 'Risicoanalyse'),
	(12, 2, 2, 'Technologieanalyse'),
	(13, 2, 3, 'Bedrijfsanalyse'),
	(14, 2, 4, 'Procesanalyse'),
	(15, 2, 5, 'Informatieanalyse'),
	(16, 2, 6, 'Requirementsanalyse'),
	(17, 3, 0, 'Algemeen'),
	(18, 3, 1, 'Informatiemodel'),
	(19, 3, 2, 'Procesverbetering'),
	(20, 3, 3, 'Systeemarchitectuur'),
	(21, 3, 4, 'Applicaties'),
	(22, 3, 5, 'Testen'),
	(23, 3, 6, 'User interface'),
	(24, 4, 0, 'Algemeen'),
	(25, 4, 1, 'Databases'),
	(26, 4, 2, 'Procesverbetering'),
	(27, 4, 3, 'Pakketconfiguratie'),
	(28, 4, 4, 'Applicaties'),
	(29, 4, 5, 'Testen'),
	(30, 4, 6, 'User interface'),
	(31, 6, 0, 'Algemeen'),
	(33, 7, 1, 'Mondeling'),
	(34, 7, 2, 'Schriftelijk'),
	(35, 7, 3, 'Presenteren'),
	(36, 7, 4, 'Interviewen'),
	(37, 7, 5, 'Engels'),
	(38, 7, 6, 'Contextueel'),
	(39, 8, 0, 'Algemeen'),
	(40, 8, 1, 'Feedback'),
	(41, 8, 2, 'Organiseren'),
	(42, 8, 3, 'Leiderschap'),
	(43, 8, 4, 'Vergaderen'),
	(44, 8, 5, 'Intercultureel'),
	(45, 9, 0, 'Algemeen'),
	(46, 9, 1, 'Eigen ontwikkeling'),
	(47, 9, 2, 'Reflectie'),
	(48, 9, 3, 'Kennisontwikkeling')
) AS Source (Id, CompetentieId, Volgnummer, Naam)  
ON Target.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id, CompetentieId, Volgnummer, Naam) VALUES (Id, CompetentieId, Volgnummer, Naam)
WHEN MATCHED THEN
	UPDATE SET
		CompetentieId = Source.CompetentieId,
		Volgnummer = Source.Volgnummer,
		Naam = Source.Naam;

SET IDENTITY_INSERT Competenties.CompetentieOnderdeel OFF