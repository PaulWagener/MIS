SET IDENTITY_INSERT Competenties.Kwaliteitskenmerk ON

MERGE INTO Competenties.Kwaliteitskenmerk AS Target  
USING (VALUES 
	(1, 1, 1, 'Doel / Deelstappen / fasering geïdentificeerd'),
	(2, 1, 2, 'Keuze voor aanpak is beargumenteerd'),
	(3, 1, 3, 'Beoogde kwaliteit is vastgesteld'),
	(4, 1, 4, 'Risicoanalyse met tegenmaatregelen'),
	(5, 1, 5, 'Benodigd budget is vastgesteld'),
	(6, 2, 1, 'Juiste opsplitsing in deeltaken'),
	(7, 2, 2, 'Schattingen zijn realistisch'),
	(8, 2, 3, 'Tijdsbesteding is gemeten'),
	(9, 2, 4, 'Inzichten uit verleden worden meegenomen'),
	(10, 2, 5, 'Voortgang inzichtelijk gemaakt'),
	(11, 2, 6, 'Planning wordt bijgesteld'),
	(12, 3, 1, 'Keuzes beheerplan komen aantoonbaar voort uit de beheersituatie'),
	(13, 3, 2, 'beheerplan is onderverdeeld in relevante onderdelen: infrastructuur, applicaties, functioneel'),
	(14, 3, 3, 'SLA is aantoonbaar afgeleid uit het beheerplan'),
	(15, 3, 4, 'SLA is volledig t.a.v. de componenten'),
	(16, 4, 1, 'o.b.v. beargumenteerde keuze voor implementatiestrategie'),
	(17, 4, 2, 'Scope en impact zijn geidentificeerd'),
	(18, 4, 3, 'Benodigde mensen en middelen zijn begroot'),
	(19, 4, 4, 'risico''s, impact en tegenmaatregelen van implementatie zijn duidelijk'),
	(20, 4, 5, 'Fasen en activiteiten zijn gepland'),
	(21, 5, 1, '?'),
	(22, 5, 2, '?'),
	(23, 6, 1, 'Beargumenteerde afweging tussen lineair/analytisch en iteratief/incrementeel gemaakt'),
	(24, 6, 2, 'Standaardmethodiek wordt gevolgd'),
	(25, 7, 1, 'IDE omgeving wordt correct gebruikt'),
	(26, 7, 2, 'Testen is geautomatiseerd'),
	(27, 7, 3, 'Ontwikkelomgeving is overdraagbaar'),
	(28, 8, 1, '?'),
	(29, 8, 2, '?'),
	(30, 9, 1, 'Tool (correct) gebruikt'),
	(31, 9, 2, 'Branching bepaalt'),
	(32, 9, 3, 'Change control toegepast'),
	(33, 9, 4, 'Componenten zijn geidentificeerd'),
	(34, 9, 5, 'BOM van releases zijn gemanaged'),
	(35, 9, 6, 'Koppeling met requirements bewaakt')
) AS Source (Id, CompetentieOnderdeelId, Volgnummer, Omschrijving)  
ON Target.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id, CompetentieOnderdeelId, Volgnummer, Omschrijving) VALUES (Id, CompetentieOnderdeelId, Volgnummer, Omschrijving)
WHEN MATCHED THEN
	UPDATE SET
		CompetentieOnderdeelId = Source.CompetentieOnderdeelId,
		Volgnummer = Source.Volgnummer,
		Omschrijving = Source.Omschrijving;

SET IDENTITY_INSERT Competenties.Kwaliteitskenmerk OFF