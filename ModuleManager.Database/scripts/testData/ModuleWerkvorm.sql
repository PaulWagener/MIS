MERGE INTO ModuleWerkvorm AS Target  
USING (values 
	(N'DATAB1', 1516, N'HC', N'Omschrijving hier...'),
	(N'DATAB1', 1516, N'PR', N'Omschrijving hier...'),
	(N'DATAB2', 1516, N'HC', N'Omschrijving hier...'),
	(N'DATAB2', 1516, N'PR', N'Omschrijving hier...'),
	(N'ICTIB', 1516, N'HC', N'Omschrijving hier...'),
	(N'ICTIB', 1516, N'WS', N'Omschrijving hier...'),
	(N'INPRJ2', 1516, N'GR', N'Omschrijving hier...'),
	(N'INPRJ2', 1516, N'IN', N'Omschrijving hier...'),
	(N'INPRJ2', 1516, N'PU', N'Omschrijving hier...'),
	(N'INPRJ2', 1516, N'TU', N'Omschrijving hier...'),
	(N'INTRVW', 1516, N'WS', N'Omschrijving hier...'),
	(N'MODL1', 1516, N'HC', N'Omschrijving hier...'),
	(N'MODL1', 1516, N'PR', N'Omschrijving hier...'),
	(N'ORI1', 1516, N'GR', N'Omschrijving hier...'),
	(N'PROG1', 1516, N'HC', N'Het hoorcollege is telkens aan het eind van de week en dient als laatste vraagbaak voor de stof van die week. Ook worden hier huiswerkopdrachten behandeld en gaan we, indien gewenst, de diepte of breedte in betreffende de stof van de afgelopen week.'),
	(N'PROG1', 1516, N'PR', N'In het practicum wordt door de docent aan het begin van de week een korte toelichting op een volgend stuk lesstof gegeven, waarna dit meteen in de praktijk gebracht wordt via (een of meer), oefeningen tijdens de workshop. De docent begeleidt de studenten daarbij en geeft  (individuele of groeps-), feedback. De opdrachten die in het practicum niet af komen dient de student thuis verder af te maken. Naast de informatie die tijdens de workshops overgedragen wordt, zal de student ook veel moeten leren door zelfstudie. Voor de practica is aanwezigheid verplicht.'),
	(N'PROG2', 1516, N'HC', N'Het hoorcollege is telkens aan het eind van de week en dient als laatste vraagbaak voor de stof van die week. Ook worden hier huiswerkopdrachten behandeld en gaan we, indien gewenst, de diepte of breedte in betreffende de stof van de afgelopen week.'),
	(N'PROG2', 1516, N'PR', N'In het practicum wordt door de docent aan het begin van de week een korte toelichting op een volgend stuk lesstof gegeven, waarna dit meteen in de praktijk gebracht wordt via (een of meer), oefeningen tijdens de workshop. De docent begeleidt de studenten daarbij en geeft  (individuele of groeps-), feedback. De opdrachten die in het practicum niet af komen dient de student thuis verder af te maken. Naast de informatie die tijdens de workshops overgedragen wordt, zal de student ook veel moeten leren door zelfstudie. Voor de practica is aanwezigheid verplicht.'),
	(N'PROG3', 1516, N'PR', NULL),
	(N'PROG3', 1516, N'TH', NULL),
	(N'PROG4', 1516, N'PR', NULL),
	(N'PROG4', 1516, N'TH', NULL),
	(N'PROG5', 1516, N'WS', N'In elke workshop worden twee thema’s behandeld. Voor elk thema zijn er PowerPoint slides en een opdrachtbeschrijving beschikbaar. Daarnaast zijn er uitwerkingen van de opdrachten en enkele demo’s gemaakt die helpen bij het uitleggen van de theorie.  Na het behandelen van het eerste thema is er tijd om aan de opdracht die bij dat thema hoort te werken. In het tweede deel van de workshop wordt dan het tweede thema behandeld en tot slot is er nog tijd om verder aan de opdrachten te werken. '),
	(N'PROG6', 1516, N'WS', N'In elke workshop worden twee thema’s behandeld. Voor elk thema zijn er PowerPoint slides en een opdrachtbeschrijving beschikbaar. Daarnaast zijn er uitwerkingen van de opdrachten en enkele demo’s gemaakt die helpen bij het uitleggen van de theorie.  Na het behandelen van het eerste thema is er tijd om aan de opdracht die bij dat thema hoort te werken. In het tweede deel van de workshop wordt dan het tweede thema behandeld en tot slot is er nog tijd om verder aan de opdrachten te werken. '),
	(N'SLC01', 1516, N'WS', N'Omschrijving hier...'),
	(N'VERGA', 1516, N'WS', N'Omschrijving hier...'),
	(N'WEBS1', 1516, N'HC', N'Het hoorcollege is telkens aan het begin van de week en dient als introductie voor de stof van die week.'),
	(N'WEBS1', 1516, N'PR', N'In het practicum wordt door de docent aan het begin van de week een korte toelichting op een volgend stuk lesstof gegeven, waarna dit meteen in de praktijk gebracht wordt via (een of meer), oefeningen tijdens de workshop. De docent begeleidt de studenten daarbij en geeft  (individuele of groeps-), feedback. De opdrachten die in het practicum niet af komen dient de student thuis verder af te maken. Naast de informatie die tijdens de hoorcolleges overgedragen wordt, zal de student ook veel moeten leren door zelfstudie. Voor de practica is aanwezigheid verplicht.'),
	(N'WEBS2', 1516, N'WS', N'In de workshop wordt door de docent steeds een korte toelichting op een volgend stuk lesstof gegeven, waarna dit meteen in de praktijk gebracht wordt via (een of meer), oefeningen tijdens de workshop. De docent begeleidt de studenten daarbij en geeft  (individuele of groeps-), feedback. Iedere workshop wordt afgesloten met het uitreiken van een huiswerkopdracht, die de studenten in duo’s moeten maken. Naast de informatie die tijdens de workshops overgedragen wordt, zal de student ook veel moeten leren door zelfstudie. Voor de workshops is aanwezigheid verplicht.'),
	(N'WEBS3', 1516, N'TH', NULL),
	(N'WEBS3', 1516, N'WS', N'Tijdens de practica wordt gewerkt aan de huiswerkopdrachten. De huiswerkopdrachten zijn richtinggevend voor het maken van de eindopdracht, de webshop.'),
	(N'WEBS4', 1516, N'WS', N'In de workshop wordt door de docent steeds een korte toelichting op een volgend stuk lesstof gegeven, waarna dit meteen in de praktijk gebracht wordt via (een of meer), oefeningen tijdens de workshop. De docent begeleidt de studenten daarbij en geeft  (individuele of groeps-), feedback. Iedere workshop wordt afgesloten met het uitreiken van een huiswerkopdracht, die de studenten in duo’s moeten maken. Naast de informatie die tijdens de workshops overgedragen wordt, zal de student ook veel moeten leren door zelfstudie. Voor de workshops is aanwezigheid verplicht.')
) AS Source ([CursusCode], [Schooljaar], [WerkvormType], [Organisatie])  
ON Target.[CursusCode] = Source.[CursusCode]
	AND Target.[Schooljaar] = Source.[Schooljaar]
	AND Target.[WerkvormType] = Source.[WerkvormType]
WHEN NOT MATCHED BY TARGET THEN  
	INSERT ([CursusCode], [Schooljaar], [WerkvormType], [Organisatie]) VALUES ([CursusCode], [Schooljaar], [WerkvormType], [Organisatie])
WHEN MATCHED THEN
	UPDATE SET [Organisatie] = Source.[Organisatie];