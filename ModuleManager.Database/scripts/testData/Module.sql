﻿MERGE INTO Module AS Target  
USING (VALUES 
	(N'DATAB1', 1516, N'Databases 1', N'Databases 1', 1, N'Compleet', 0, N'MOD1', N'code                ', N'1', 0),
	(N'DATAB2', 1516, N'Databases 2', N'Databases 2', 1, N'Compleet', 0, N'MOD2', N'code                ', N'2', 0),
	(N'ENUP1', 1516, N'Engels (B1-update)', N'Engels (B1-update)', 1, N'Compleet', 0, N'ABV1', N'code                ', N'1', 0),
	(N'ENUP2', 1516, N'Engels (B1-update)', N'Engels (B1-update)', 1, N'Compleet', 0, N'ABV2', N'code                ', N'2', 0),
	(N'ICTIB', 1516, N'ICT in Bedrijf', N'ICT in Bedrijf',2, N'Compleet', 0, N'MOD1', N'code                ', N'1', 0),
	(N'INPRJ2', 1516, N'Project Databases', N'Project Databases',3, N'Compleet', 0, N'GRW2', N'code                ', N'2', 0),
	(N'INTRVW', 1516, N'Interviewen', N'Interviewen', 1, N'Compleet', 0, N'ABV2', N'code                ', N'2', 0),
	(N'MODL1', 1516, N'Modelleren 1', N'Modelleren 1', 1, N'Compleet', 0, N'MOD2', N'code                ', N'2', 0),
	(N'ORI1', 1516, N'Oriëntatie 1', N'Oriëntatie 1', 1, N'Compleet', 0, N'GRW1', N'code                ', N'1', 0),
	(N'PROG1', 1516, N'Een van de ruggengraten van de opleiding wordt gevormd door het programmeeronderwijs. Tijdens de propedeusefase ligt de nadruk van dit programmeeronderwijs (Programmeren 1 - 4) op het visueel en objectgeoriënteerd leren programmeren in Java. In de module Programmeren 1 ligt de nadruk op het ontwikkelen van de basiskennis van de Java syntax en OO ontwikkeling.', N'Programmeren 1', 2, N'Compleet', 0, N'MOD1', N'code                ', N'1', 0),
	(N'PROG2', 1516, N'Een van de ruggengraten van de opleiding wordt gevormd door het programmeeronderwijs. Tijdens de propedeusefase ligt de nadruk van dit programmeeronderwijs (Programmeren 1 - 4) op het visueel en objectgeoriënteerd leren programmeren in Java. In de module Programmeren 1 ligt de nadruk op het ontwikkelen van de basiskennis van de Java syntax en OO ontwikkeling.', N'Programmeren 2', 2, N'Compleet', 0, N'MOD2', N'code                ', N'2', 0),
	(N'PROG3', 1516, N'Een van de ruggengraten van de opleiding wordt gevormd door het programmeeronderwijs. Tijdens de propedeusefase ligt de nadruk van dit programmeeronderwijs ( Programmeren 1 – 4) op het visueel en objectgeoriënteerd leren programmeren in Java. In de module  Programmeren 3'' ligt de nadruk meer op een verdieping van de basiskennis van Java en OO development en zetten we een eerste stap naar het ontwikkelen van Graphical User Interfaces.', N'Programmeren 3', 2, N'Compleet', 0, N'ABV1', N'code                ', N'3', 0),
	(N'PROG4', 1516, N'Een van de ruggengraten van de opleiding wordt gevormd door het programmeeronderwijs. Tijdens de propedeusefase ligt de nadruk van dit programmeeronderwijs ( Programmeren 1 – 4) op het visueel en objectgeoriënteerd leren programmeren in Java. In de module  Programmeren 3'' ligt de nadruk meer op een verdieping van de basiskennis van Java en OO development en zetten we een eerste stap naar het ontwikkelen van Graphical User Interfaces.', N'Programmeren 4', 2, N'Compleet', 0, N'ABV1', N'code                ', N'4', 0),
	(N'PROG5', 1516, N'In het tweede jaar van de opleiding wordt een start gemaakt met het ontwikkelen van software voor het Microsoft .NET platform. Hierbij gebruiken we C# als programmeertaal. De studenten hebben dan al ruime ervaring met programmeren in Java (Programmeren 1-4). In Programmeren 5 maken we gebruik van C# in Visual Studio de nadruk ligt hierbij op webdevelopment (ASP.NET MVC). Na een korte introductie van C# en Visual Studio komen de ins & outs van webdevelopment in .NET aan bod. Ook is er aandacht voor unit-tests en het werken met domain models en database access m.b.v. Entity Framework.', N'Programmeren 5', 2, N'Compleet', 0, N'ABV1', N'code                ', N'5', 0),
	(N'PROG6', 1516, N'In het tweede jaar van de opleiding wordt een start gemaakt met het ontwikkelen van software voor het Microsoft .NET platform. Hierbij gebruiken we C# als programmeertaal. De studenten hebben dan al ruime ervaring met programmeren in Java (Programmeren 1-4). Programmeren 6 volgt op Programmeren 5. In programmeren 5 ligt de nadruk op webdevelopment (ASP.NET MVC) en het werken met Visual Studio en C#. In programmeren 6 ligt de focus op het toepassen van software architectuur patterns en complexe programmeertechnieken in C#.', N'Programmeren 6', 2, N'Compleet', 0, N'ABV1', N'code                ', N'6', 0),
	(N'SLC01', 1516, N'Studieloopbaancoaching 1', N'Studieloopbaancoaching 1', 2, N'Compleet', 0, N'ABV2', N'code                ', N'2', 0),
	(N'VERGA', 1516, N'Vergaderen', N'Vergaderen', 3, N'Compleet', 0, N'ABV1', N'code                ', N'1', 0),
	(N'WEBS1', 1516, N'De module Websites bouwen 1 is een inleiding in de methoden en technieken die worden gebruikt bij het presenteren van informatie op het web. Websites worden veelal gemaakt met één van de talloze (al dan niet gratis verkrijgbare) programma''s die daarvoor speciaal ontwikkeld zijn.In deze module gaan we niet met die hulpmiddelen werken, maar leer je de achterliggende basisprincipes van HTML (HyperText Markup Language) en CSS (Cascading Style Sheets). Na afloop van de module begrijp je deze basis en ben je in staat om zelfstandig een al dan niet interactieve website te ontwikkelen, zonder gebruik te maken van specifieke tools. Deze module richt zich op de ontwikkeling van de client-side van websites. Je hebt hiervoor niet per se een webserver nodig. De kennis die je in deze module opdoet, heb je in het vervolg van de studie nodig om server-side webapplicaties te kunnen maken, waarmee je bijvoorbeeld gegevens uit een database op het web kunt presenteren en via webformulieren kunt laten wijzigen.', N'Websites bouwen', 4, N'Compleet', 0, N'MOD1', N'code                ', N'1', 0),
	(N'WEBS2', 1516, N'In het 1e jaar heb je al kennis gemaakt met HTML en CSS, de talen waarmee je overwegend statische websites maakt. In deze module maak je kennis met de wereld van dynamische websites. Om de aandacht van de internet gebruikers te trekken worden er dynamische talen gebruikt, zoals JavaScript. Deze talen draaien aan de client kant. Om diverse redenen worden er ook aan de server kant programma’s gemaakt. Dergelijke programma''s kunnen bijvoorbeeld in ASP/ASP.NET (Microsoft) of in PHP (open source) geschreven worden. Omdat je dergelijke talen in je stage en later in de praktijk tegen kunt en zult komen, gaan we daar in deze module mee aan de slag. Meer specifiek gebruiken we in deze module PHP en jQuery (Javascript framework), waarbij we MySQL als achterliggende database zullen gebruiken.', N'PHP', 3, N'Compleet', 0, N'ABV1', N'code                ', N'7', 0),
	(N'WEBS3', 1516, N'In het vak Websites 2 hebben studenten kennis gemaakt met PHP en jQuery. In dit vak gaan de studenten een PHP-framework gebruiken: Laravel. Verder gaan ze Javascript wat meer uitdiepen als onderliggende taal van jQuery.', N'Javascript', 3, N'Compleet', 0, N'ABV1', N'code                ', N'8', 0),
	(N'WEBS4', 1516, N'Websites 4 gaat over XML, en als zodanig dekt de modulenaam WEBS4 niet geheel de lading.	XML is je al bekend van Websites 1, omdat je daar o.a. XHTML hebt geleerd. Maar dat er veel meer achter die tags zit dan je op het eerste gezicht zou vermoeden wordt in deze cursus duidelijk.	Je leert zelf XML-bestandsformaten bedenken, die je vervolgens beschrijft met een DTD of een Schema (XSD), waardoor de syntax vastligt. Met externe tools kun je dan controleren of een gegeven document in dat formaat geldig is, of dat er wellicht syntactische of semantische fouten in zitten.	Ook leer je XML-formaten om te zetten in andere tekst-formaten met XSL-transformaties. In het verlengde daarvan leer je ook een veelgebruikt XML-formaat voor pagina-opmaak: de Formatting Objects (XSL-FO), waarmee je via een tool pdf kunt genereren. Natuurlijk gebruik je dan vooraf XSLT om van een willekeurig XML-document eerst XSL-FO te maken. Tenslotte leer je hoe je XML-documenten kunt lezen m.b.v. standaard beschikbare parsers. Ook die hoef je dus niet zelf te schrijven. Voor gebruik bij de XSL-transformaties en de parsers leer je ook een query-taal: XPath.', N'XML', 4, N'Compleet', 0, N'ABV1', N'code                ', N'8', 0)
) AS Source (CursusCode, Schooljaar, Beschrijving, Naam, VerantwoordelijkeDocentId, Status, Gecontroleerd, OnderdeelCode, Icon, Blok, [ReadOnly])
ON 
	Target.CursusCode = Source.CursusCode
	AND Target.Schooljaar = Source.Schooljaar
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (CursusCode, Schooljaar, Beschrijving, Naam, VerantwoordelijkeDocentId, Status, Gecontroleerd, OnderdeelCode, Icon, Blok, [ReadOnly]) 
	VALUES (CursusCode, Schooljaar, Beschrijving, Naam, VerantwoordelijkeDocentId, Status, Gecontroleerd,OnderdeelCode, Icon, Blok, [ReadOnly])
WHEN MATCHED THEN
	UPDATE SET
		Beschrijving = Source.Beschrijving, 
		Naam = Source.Naam, 
		VerantwoordelijkeDocentId = Source.VerantwoordelijkeDocentId,
		Status = Source.Status, 
		OnderdeelCode = Source.OnderdeelCode, 
		Icon = Source.Icon,
		Blok = Source.Blok,
		[ReadOnly] = Source.[ReadOnly];