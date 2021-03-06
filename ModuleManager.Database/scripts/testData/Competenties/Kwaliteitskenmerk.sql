﻿SET IDENTITY_INSERT Competenties.Kwaliteitskenmerk ON

MERGE INTO Competenties.Kwaliteitskenmerk AS Target  
USING (VALUES 
	/* Beheren */
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
	(35, 9, 6, 'Koppeling met requirements bewaakt'),
	/* Analyseren*/
	(36, 10, 1, 'Volledigheid t.a.v. scope'),
	(37, 10, 2, 'Oorzaak en gevolg zijn gescheiden'),
	(38, 10, 3, 'Scope is helder gedefinieerd'),
	(39, 10, 4, 'Hoofd- en deelproblemen onderkend en geprioriteriseerd'),
	(40, 10, 5, 'Stakeholders zijn geidentificeerd'),
	(41, 10, 6, 'Probleem is geidentifceerd los van mogelijke oplossingen'),
	(42, 10, 7, 'Consistentie over deelanalyses'),
	(43, 11, 1, 'Impact en waarschijnlijkheid bepaald'),
	(44, 11, 2, 'Maatregelen benoemd'),
	(45, 11, 3, 'Onderscheid tussen project- en productrisico''s gemaakt'),
	(46, 11, 4, 'Faalkans van risico''s in software / oplossing bepaald'),
	(47, 11, 5, 'Dreigingen zijn onderkend en vertaalt naar risico''s'),
	(48, 12, 1, 'Actuele ontwikkelingen meegenomen in analyse'),
	(49, 12, 2, 'Benodigde functionele en/of technische infrastructuur voor technologie/product bekeken'),
	(50, 12, 3, '(Technologische) mogelijkheden zijn onderzocht'),
	(51, 12, 4, 'Expertkennis van anderen gebruikt'),
	(52, 12, 5, 'Analyse is uitgevoerd in de context'),
	(53, 12, 6, 'Voordelen mogelijkheden van technologien/producten zijn verduidelijkt'),
	(54, 13, 1, 'Bedrijfsspecifieke SW en ICT oplossingen zijn herkend'),
	(55, 13, 2, 'Bedrijf is op hoofdlijnen weergegeven'),
	(56, 13, 3, 'Analyse is uitgevoerd o.b.v. besturingsmodel'),
	(57, 13, 4, 'Samenhangende informatiebehoefte en informatieverwerkende activiteiten zijn onderkend'),
	(58, 13, 5, 'Uitgevoerd voor proces- en informatieanaylse'),
	(59, 13, 6, 'Correcte opsplitsing in operationele en tactische problemen'),
	(60, 13, 7, 'Correct gebruik van bedrijfskundige terminologie'),
	(61, 13, 8, 'Koppeling met context/situatie is gelegd'),
	(62, 14, 1, 'Consistent met bedrijfsanalyse'),
	(63, 14, 2, 'Procesmodellen zijn passend voor probleemstelling'),
	(64, 14, 3, 'Procesmodellen zijn leesbaar voor belanghebbenden'),
	(65, 14, 4, 'Relatie tussen problemen in informatievoorziening en knelpunten in bedrijfsvoering gelegd'),
	(66, 14, 5, 'Effectiviteit, Problemen/knelpunten (van 1 proces) zijn geidentificeerd'),
	(67, 14, 6, 'Modelleringstechniek is correct toegepast'),
	(68, 14, 7, 'Activiteiten binnen processen zijn correct gemodelleerd'),
	(69, 14, 8, 'Besturing van processen zijn correct gemodelleerd'),
	(70, 14, 9, 'Business rules zijn opgesteld'),
	(71, 15, 1, 'Consistent met bedrijfs- en procesanalyse'),
	(72, 15, 2, 'Knelpunten veroorzaakt door de informatievoorziening zijn onderkend'),
	(73, 15, 3, 'Informatievoorzieningen geanalyseerd o.b.v. informatiebehoeften'),
	(74, 16, 1, 'Randvoorwaarden beschreven'),
	(75, 16, 2, 'Functionele en niet-functionele requirements beschreven'),
	(76, 16, 3, 'Samenhang oplossing/behoefte is duidelijk'),
	(77, 16, 4, 'Richtlijnen 1 t/m 14 van begin bij het einde zijn gebruikt [AANPASSEN]'),
	(78, 16, 5, 'Product use cases gemaakt o.b.v. Business Use cases'),
	(79, 16, 6, 'Requirements zijn helder beschreven (gebaseerd op PUC, prioriteit, SMART, functioneel, non-functioneel, rationale)'),
	(80, 16, 7, 'Beeld van eindgebruiker gevormd'),
	(81, 16, 8, 'Business rules zijn gedefinieerd'),
	(82, 17, 1, 'Correcte toepassing van standaard-modelleringstechnieken'),
	/* Ontwerpen */
	(83, 17, 2, 'Koppeling met functioneel ontwerp / requirements gemaakt'),
	(84, 17, 3, 'Juiste verhouding tussen details en abstractie '),
	(85, 17, 4, 'Keuzes zijn onderbouwd'),
	(86, 17, 5, 'Kan ontwerp iteratief creëren middels refactoringslagen (Bij Agile)'),
	(87, 17, 6, 'Volledig en samenhangend'),
	(88, 17, 7, 'Passend binnen een bestaand systeem, platform of framework'),
	(89, 17, 8, 'Juistheid van ontwerp gecontroleerd bij opdrachtgever'),
	(90, 17, 9, 'Rekening gehouden met belanghebbenden'),
	(91, 17, 10, 'Correcte toepassing standaard ontwerptechnieken'),
	(92, 18, 1, 'Compleet t.a.v. UvD'),
	(93, 18, 2, 'Niet-redundant'),
	(94, 18, 3, 'Efficiënt'),
	(95, 18, 4, 'Correct relationeel model'),
	(96, 18, 5, 'Business-rules/data-integriteit geformuleerd'),
	(97, 19, 1, 'Verbeterpunten / oplossingen zijn beschreven'),
	(98, 19, 2, 'Consistent met probleemanalyse'),
	(99, 19, 3, 'Passende ICT-oplossing gekozen'),
	(100, 19, 4, 'Datastructuren zijn op de juiste manier aan processtappen gekoppeld'),
	(101, 19, 5, 'Bruikbare oplossing onderzocht middels pakketselectie'),
	(102, 19, 6, '(Negatieve) impact van verandering is duidelijk'),
	(103, 19, 7, 'Correct rapportageontwerp voor management'),
	(104, 19, 8, 'Gegevens zijn omgezet naar informatie/inzicht'),
	(105, 20, 1, 'Beveiligingsaspecten benoemd en geadresseerd'),
	(106, 20, 2, 'Deploymentmodel beschreven'),
	(107, 20, 3, 'Bestaande componenten zijn geintegreerd'),
	(108, 21, 1, 'OO-principes toegepast'),
	(109, 21, 2, 'Gelaagdheid correct toegepast'),
	(110, 21, 3, 'Oog voor herbruikbaar, uitbreidbaar- en onderhoudbaarheid'),
	(111, 21, 4, 'Ontwerp is modulair'),
	(112, 21, 5, 'Standaard patronen toegepast'),
	(113, 21, 6, 'Rekening gehouden met onderhoudbaarheid'),
	(114, 21, 7, 'Rekening gehouden met uitbreidbaarheid'),
	(115, 21, 8, 'MVC architectuur toegepast'),
	(116, 22, 1, 'Adequate teststrategie gekozen (Risicoklasse, testzwaarte, testvorm)'),
	(117, 22, 2, 'Acceptatietest dekt de requirements'),
	(118, 22, 3, 'Onderscheid tussen functioneel, niet-functioneel en randvoorwaarde'),
	(119, 22, 4, 'Past standaard testtechnieken toe'),
	(120, 22, 5, 'Testplan voldoet aan teststrategie'),
	(121, 23, 1, 'Workflow ontwerpen gemaakt obv informatiearchitectuur'),
	(122, 23, 2, 'Interface Design-principes of -patronen gebruikt'),
	(123, 23, 3, 'inzichten uit de cognitieve psychologie gebruikt bij schermontwerp'),
	(124, 23, 4, 'Stijlgidsen gebruikt'),
	(125, 23, 5, 'Schermnavigatie is duidelijk'),
	(126, 23, 6, 'Interaction Design Patterns gebruikt'),
	/* Realiseren */
	(127, 24, 1, 'In overeenstemming met het ontwerp'),
	(128, 24, 2, 'Functionaliteit voldoet aan requirements en is bruikbaar'),
	(129, 24, 3, 'Oplossing is gedocumenteerd en overdraagbaar'),
	(130, 24, 4, 'Realisatiekeuzes zijn onderbouwd'),
	(131, 24, 5, 'Oplossing is volledig'),
	(132, 24, 6, 'Oplossing is stabiel'),
	(133, 24, 7, 'Passend binnen een bestaand systeem, platform of framework'),
	(134, 25, 1, 'Keuze voor type database beargumenteerd'),
	(135, 25, 2, 'Correcte vertaling naar tabellen'),
	(136, 25, 3, 'Genormaliseerd en gevuld met relevante data'),
	(137, 25, 4, 'Queries en/of rapportages leveren relevante en correcte informatie'),
	(138, 25, 5, 'Database is beheerbaar'),
	(139, 25, 6, 'Business-rules geïmplementeerd'),
	(140, 25, 7, 'Data-integriteit is geborgd'),
	(141, 25, 8, 'Oplossing biedt mogelijkheden tot Datamining'),
	(142, 25, 9, 'Passende lay-out voor standaard management rapportage'),
	(143, 25, 10, 'SQL queries zijn syntactisch correct'),
	(144, 27, 1, 'Concrete situatie vertaalt in parametrisering'),
	(145, 27, 2, 'Correct gebruik functionaliteit en ''architectuur'' van gestandardiseerd tool'),
	(146, 27, 3, 'Datamigratie correct uitgevoerd'),
	(147, 28, 1, 'Vakkundige programmeercode (coding conventions, correctheid, commentaar)'),
	(148, 28, 2, 'Programmeercode is onderhoudbaar'),
	(149, 28, 3, 'Programmeercode is herbruikbaar'),
	(150, 28, 4, 'Multithreaded, asynchroon waar nodig'),
	(151, 28, 5, 'Goede performance'),
	(152, 28, 6, 'Functionaliteit is aangetoond middels prototyping'),
	(153, 28, 7, 'Correcte keuze voor Datastructuren en Algoritmen'),
	(154, 29, 1, 'Testcases zijn vastgelegd'),
	(155, 29, 2, 'Voldoende mate van code coverage bereikt'),
	(156, 29, 3, 'Bruikbaarheid van oplossing is getest middels acceptatietest + stabiliteit ?'),
	(157, 29, 4, 'Useability van oplossing en userinterface is getest'),
	(158, 29, 5, 'Kwaliteit van oplossing is gestest middels Unit tests'),
	(159, 29, 6, 'Kwaliteit van oplossing is gestest middels Systeem test'),
	(160, 29, 7, 'Kwaliteit van oplossing is gestest middels Integratie tests'),
	(161, 29, 8, 'Tests zijn geautomatiseerd'),
	(162, 29, 9, 'Resultaten van tests zijn gedocumenteerd'),
	(163, 29, 10, 'Tests dekken de requirements'),
	/* Adviseren */
	(164, 31, 1, 'Gewenste rol als adviseur aangenomen'),
	(165, 31, 2, 'Type adviesvraag is herkend'),
	(166, 31, 3, 'Stakeholders zijn geidentificeerd'),
	(167, 31, 4, 'Advies past bij adviesvraagtype'),
	(168, 31, 5, 'Juiste invalshoek / belang gebruikt'),
	(169, 31, 6, 'Volledig'),
	(170, 31, 7, 'Advies biedt oplossing voor het probleem'),
	(171, 31, 8, 'Consistent met probleemanalyse'),
	(172, 31, 9, 'Aanbevelingen zijn onderbouwd'),
	/* Communiceren */
	(173, 33, 1, 'Beheerst de basisgesprekstechnieken'),
	(174, 33, 2, 'Kent het verschil tussen verbale vs non-verbale communicatie'),
	(175, 33, 3, 'Bepaalt en bewaakt het gespreksdoel'),
	(176, 33, 4, 'Bepaalt en bewaakt de gespreksstructuur'),
	(177, 33, 5, 'Past stijl aan aan gesprekspartner en situatie'),
	(178, 33, 6, 'Kan gevoelens genuanceerd uiten en adequaat reageren op gevoelsuitingen van anderen'),
	(179, 33, 7, 'Kan in een discussie zijn/haar mening naar voren brengen, verantwoorden en overeind houden'),
	(180, 33, 8, 'Kan complexe informatie en adviezen met betrekking tot het eigen vak- of interessegebied uitwisselen'),
	(181, 34, 1, 'Beheerst verschillende tekstvormen'),
	(182, 34, 2, 'Beheerst de verplichte onderdelen van een zakelijk rapport'),
	(183, 34, 3, 'Kan schrijven op basis van centrale vraag'),
	(184, 34, 4, 'Kan schrijven op basis van een zelfstandig gevormd structuurschema'),
	(185, 34, 5, 'Kan schrijven op basis van een voorgeschreven structuur'),
	(186, 34, 6, 'Beargumenteert en onderbouwt gemaakte keuzes'),
	(187, 34, 7, 'Vermeldt bronnen volgens APA-richtlijnen in hoofdtekst en literatuurlijst'),
	(188, 34, 8, 'Vermijdt taal- en spelfouten'),
	(189, 35, 1, 'Voorziet de presentatie van de juiste opbouw'),
	(190, 35, 2, 'Richt de presentatie in aan de hand van een duidelijke doelstelling'),
	(191, 35, 3, 'Kies de juiste hulpmiddelen, passend bij het doel van de presentatie'),
	(192, 35, 4, 'Past de presentatie inhoudelijk aan aan doelgroep'),
	(193, 35, 5, 'Past het taalgebruik tijdens de presentatie aan aan doelgroep'),
	(194, 35, 6, 'Heeft een professionele houding en stemgebruik'),
	(195, 35, 7, 'Betrekt het publiek bij de presentatie'),
	(196, 36, 1, 'Richt zich op de vraag van de klant'),
	(197, 36, 2, 'Onderzoekt met behulp van interviewtechnieken de klantvraag'),
	(198, 36, 3, 'Bereidt het interview voor op basis van hoofdthema en subthema''s'),
	(199, 36, 4, 'Bepaalt en bewaakt de doelen per (sub)thema'),
	(200, 36, 5, 'Past de juiste gesprektechnieken toe om de interviewdoelen te bereiken'),
	(201, 37, 1, 'Beheerst passieve vaardigheden op B2-niveau in de Engelse taal'),
	(202, 37, 2, 'Beheerst actieve vaardigheden op B2-niveau in de Engelse taal'),
	(203, 37, 3, 'Maakt gebruik van professioneel en formeel vocabulair <= in ICT beroepscontext ?'),
	(204, 37, 4, 'Verbindt tekst aan elkaar door middel van "linking words"'),
	(205, 37, 5, 'Vermijdt grammaticale en veelgemaakte taalfouten'),
	(206, 38, 1, 'Kent de verbale en non-verbale communicatieve gebruiken van gesprekspartner(s)'),
	(207, 38, 2, 'Formuleert een genuanceerde mening in een cultureel debat'),
	(208, 38, 3, 'Past de communicatie aan aan de situatie/context'),
	(209, 38, 4, 'Heeft inzicht in non-verbale communicatie en reageert daarop'),
	/* Samenwerken */
	(210, 39, 1, 'Kan de theorie van groepsvorming gebruiken om het eigen groepsproces te herkennen'),
	(211, 39, 2, 'Kan het groepsproces van de eigen groep sturen'),
	(212, 39, 3, 'Maakt duidelijke samenwerkingsafspraken met de groep'),
	(213, 39, 4, 'Kent de verschillende groepsrollen en heeft inzicht in eigen voorkeursrol(len)'),
	(214, 39, 5, 'Houdt rekening met de individuele verschillen in de groep'),
	(215, 39, 6, 'Beschikt over voldoende inhoudelijke kennis om effectief sament te kunnen werken'),
	(216, 39, 7, 'Komt op tijd op afspraken'),
	(217, 39, 8, 'Ondersteund anderen'),
	(218, 39, 9, 'Houdt een positieve werksfeer in stand'),
	(219, 39, 10, 'Deelt informatie, ideeën en ervaringen'),
	(220, 39, 11, 'Is flexibel'),
	(221, 39, 12, 'Communiceert tijdig bij problemen'),
	(222, 39, 13, 'Kan omgaan met conflicten binnen de samenwerking'),
	(223, 39, 14, 'Kan knelpunten in de samenwerking identificeren en oplossen.'),
	(224, 39, 15, 'Weet succesfactoren en niet-succesfactoren te benoemen t.a.v. groepssamenwerking'),
	(225, 39, 16, 'Evalueert de samenwerking na afloop van een samenwerkingsverband'),
	(226, 40, 1, 'Kan eigen werk en dat van anderen reviewen / bediscussiëren'),
	(227, 40, 2, 'Is kritisch op eigen functioneren en dat van anderen'),
	(228, 40, 3, 'Benoemt het effect van gedrag en houding (van zichzelf en anderen) op het samenwerkingsproces'),
	(229, 40, 4, 'Geeft gewenst gedrag aan bij anderen'),
	(230, 40, 5, 'Staat open voor feedback van anderen'),
	(231, 41, 1, 'Zelfstandig'),
	(232, 41, 2, 'Organiseert werk: stelt prioriteiten, maakt een planning en een taakververdeling'),
	(233, 41, 3, 'Stemt regelmatig en effectief af met betrokkenen'),
	(234, 41, 4, 'Heeft actieve rol in besluitvorming'),
	(235, 41, 5, 'Geeft aan wat de voortgang van eigen werk is'),
	(236, 41, 6, 'Checkt wat de voortgang van het werk van anderen is'),
	(237, 41, 7, 'Vraag tijdig hulp en ondersteuning'),
	(238, 41, 8, 'Is verantwoordelijk voor gemaakte afspraken'),
	(239, 41, 9, 'Is verantwoordelijk voor eigen bijdrage'),
	(240, 41, 10, 'Komt afspraken na'),
	(241, 42, 1, 'Kent zijn eigen voorkeursstijl'),
	(242, 42, 2, 'Kan zijn leiderschapsstijl (situationeel) aanpassen'),
	(243, 42, 3, 'Neemt besluiten die de belangen van de klant / het project behartigen'),
	(244, 43, 1, 'Beheerst de verschillende vergaderrollen'),
	(245, 43, 2, 'Kan een vergadering voorzitten'),
	(246, 43, 3, 'Kan een vergadering notuleren'),
	(247, 43, 4, 'Streeft naar heldere doelstellingen'),
	(248, 43, 5, 'Komt tot besluitvorming'),
	(249, 43, 6, 'Uitkomsten worden vastgelegd'),
	(250, 43, 7, 'Heeft inbreng tijdens overlegmomenten'),
	(251, 44, 1, 'Kent de interculturele verschillen in samenwerkings- en omgangvormen'),
	(252, 44, 2, 'Past zich in de samenwerking aan interculturele verschillen aan >>wordt dit afgetoetst?'),
	(253, 44, 3, 'Past interculturele sensitiviteit toe bij interculturele verhandelingen'),
	(254, 44, 4, 'Reflecteert op eigen culturele normen en waarden in een professionele context'),
	(255, 44, 5, 'Is bekend met culturele theorie en de toepasbaarheid bij culturele samenwerking'),
	/* Professionaliseren */
	(256, 45, 1, 'Intrinsiek gemotiveerd'),
	(257, 45, 2, 'Neemt initiatief in de organisatie waarbinnen hij/zij werkt'),
	(258, 45, 3, 'Gaat op adequate wijze om met zijn eigen verantwoordelijkheden en bevoegdheden'),
	(259, 45, 4, 'Gaat op adequate wijze om met stress'),
	(260, 45, 5, 'Heeft een assertieve houding'),
	(261, 46, 1, 'Heeft beeld van de beroepspraktijk'),
	(262, 46, 2, 'Kent gewenste startpositie in arbeidsmarkt en kan toekomstperspectief schetsen (futureproof, ook na start als professional)'),
	(263, 46, 3, 'Kent competentieset/BoKS van het werkveld'),
	(264, 46, 4, 'Kent beoogde eindniveau van competentieset/BoKS binnen de opleiding'),
	(265, 46, 5, 'Werkt planmatig / zelfverantwoordelijk aan verbetering van eigen competentieset/BoKS (ook keuzes tijdens opleiding)'),
	(266, 46, 6, 'Maakt gebruik van input van anderen m.b.t.eigen ontwikkeling'),
	(267, 46, 7, 'Heeft beeld van eigen sterktes en zwaktes'),
	(268, 46, 8, 'Weet succesfactoren en niet-succesfactoren te benoemen t.a.v. persoonlijk functioneren'),
	(269, 47, 1, 'Is kritisch op eigen handelen'),
	(270, 47, 2, 'Formuleert concrete verbetermogelijkheden'),
	(271, 47, 3, 'Laat verbeteringen zien'),
	(272, 48, 1, 'Is in staat zelfstandig (ontbrekende) informatie te vinden (literatuur, mensen uit de organisatie, standaard informatiebronnen)'),
	(273, 48, 2, 'Is in staat zich nieuwe theorieën eigen te maken en toe te passen'),
	(274, 48, 3, 'Heeft inzicht in eigen leerstijl')
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