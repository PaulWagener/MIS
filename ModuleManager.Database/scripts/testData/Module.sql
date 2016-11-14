MERGE INTO Module AS Target  
USING (VALUES 
	('PROG1', 1516, '', 'Programmeren 1', 'Ger Saris', 'Nieuw', 'ABV1', 'code', '1', 0),
	('PROG2', 1516, '', 'Programmeren 2', 'Ger Saris', 'Nieuw', 'ABV1', 'code', '2', 0),
	('PROG3', 1516, '', 'Programmeren 3', 'Ger Saris', 'Nieuw', 'ABV1', 'code', '3', 0),
	('PROG4', 1516, '', 'Programmeren 4', 'Ger Saris', 'Nieuw', 'ABV1', 'code', '4', 0),
	('PROG5', 1516, '', 'Programmeren 5', 'Stijn Smulders', 'Nieuw', 'ABV1', 'code', '5', 0),
	('PROG6', 1516, '', 'Programmeren 6', 'Stijn Smulders', 'Nieuw', 'ABV1', 'code', '6', 0),
	('WEBS1', 1516, '', 'Websites bouwen', 'Paul Wagener', 'Nieuw', 'ABV1', 'code', '1', 0),
	('WEBS2', 1516, '', 'PHP', 'Stijn Smulders', 'Nieuw', 'ABV1', 'code', '7', 0),
	('WEBS3', 1516, '', 'Javascript', 'Stijn Smulders', 'Nieuw', 'ABV1', 'code', '8', 0),
	('WEBS4', 1516, '', 'XML', 'Stijn Smulders', 'Nieuw', 'ABV1', 'code', '8', 0)
) AS Source (CursusCode, Schooljaar, Beschrijving, Naam, Verantwoordelijke, Status, OnderdeelCode, Icon, Blok, [ReadOnly])  
ON 
	Target.CursusCode = Source.CursusCode
	AND Target.Schooljaar = Source.Schooljaar
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (CursusCode, Schooljaar, Beschrijving, Naam, Verantwoordelijke, Status, Gecontroleerd, OnderdeelCode, Icon, Blok, [ReadOnly]) 
	VALUES (CursusCode, Schooljaar, Beschrijving, Naam, Verantwoordelijke, Status, 0			,OnderdeelCode, Icon, Blok, [ReadOnly])
WHEN MATCHED THEN
	UPDATE SET
		Beschrijving = Source.Beschrijving, 
		Naam = Source.Naam, 
		Verantwoordelijke = Source.Verantwoordelijke,
		Status = Source.Status, 
		OnderdeelCode = Source.OnderdeelCode, 
		Icon = Source.Icon,
		Blok = Source.Blok,
		[ReadOnly] = Source.[ReadOnly];