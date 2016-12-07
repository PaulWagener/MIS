﻿MERGE INTO Studiebelasting AS Target  
USING (values 
	(N'DATAB1', 1516, N'HC', 1, N'7', N'1', 7),
	(N'DATAB1', 1516, N'PR', 2, N'7', N'1', 14),
	(N'DATAB2', 1516, N'HC', 1, N'7', N'1', 7),
	(N'DATAB2', 1516, N'PR', 2, N'7', N'1', 14),
	(N'ENUP1', 1516, N'WS', 17, N'1', N'1', 17),
	(N'ENUP2', 1516, N'WS', 24, N'1', N'1', 24),
	(N'ICTIB', 1516, N'HC', 1, N'4', N'1', 4),
	(N'ICTIB', 1516, N'WS', 2, N'5', N'1', 10),
	(N'INPRJ2', 1516, N'PU', 2, N'7', N'1', 14),
	(N'INPRJ2', 1516, N'TU', 1, N'7', N'1', 7),
	(N'INTRVW', 1516, N'WS', 2, N'4', N'1', 8),
	(N'MODL1', 1516, N'HC', 1, N'3', N'1', 3),
	(N'MODL1', 1516, N'PR', 2, N'7', N'1', 14),
	(N'ORI1', 1516, N'GR', 2, N'8', N'1', 16),
	(N'ORI1', 1516, N'PU', NULL, N'2', N'1', 2),
	(N'PROG1', 1516, N'HC', 1, N'7', N'1', 7),
	(N'PROG1', 1516, N'HW', NULL, N'7', N'9', 63),
	(N'PROG1', 1516, N'PR', 4, N'7', N'2X2', 28),
	(N'PROG1', 1516, N'Tentamen', NULL, N'1', N'1', 14),
	(N'PROG2', 1516, N'Assessment', NULL, N'1', N'1', 23),
	(N'PROG2', 1516, N'HC', 1, N'7', N'1', 7),
	(N'PROG2', 1516, N'HW', NULL, N'6', N'1', 54),
	(N'PROG2', 1516, N'PR', 4, N'7', N'2x2', 28),
	(N'PROG3', 1516, N'HC', 7, N'7', N'1', 7),
	(N'PROG3', 1516, N'HW', NULL, N'7', N'1', 63),
	(N'PROG3', 1516, N'PR', 4, N'7', N'2x2', 28),
	(N'PROG3', 1516, N'Tentamen', NULL, N'-', N'-', 14),
	(N'PROG4', 1516, N'Assessment', NULL, N'1', N'1', 28),
	(N'PROG4', 1516, N'HC', 6, N'6', N'1', 6),
	(N'PROG4', 1516, N'Huiswerk', NULL, N'6', N'1', 54),
	(N'PROG4', 1516, N'PR', 4, N'6', N'2x2', 24),
	(N'PROG5', 1516, N'Assessment', NULL, N'1', N'1', 24),
	(N'PROG5', 1516, N'HW', 5, N'4', N'1', 20),
	(N'PROG5', 1516, N'WS', 3, N'4', N'1', 12),
	(N'PROG6', 1516, N'Assessment', NULL, N'1', N'1', 24),
	(N'PROG6', 1516, N'HW', NULL, N'4', N'1', 20),
	(N'PROG6', 1516, N'WS', 12, N'4', N'4x3', 12),
	(N'SLC01', 1516, N'WS', 1, N'7', N'1', 7),
	(N'VERGA', 1516, N'WS', 1, N'6', N'1', 6),
	(N'WEBS1', 1516, N'Eindopdracht', NULL, N'1', N'1', 35),
	(N'WEBS1', 1516, N'HC', 1, N'7', N'1', 7),
	(N'WEBS1', 1516, N'PR', 2, N'7', N'1', 14),
	(N'WEBS1', 1516, N'Zelfstudie & afmaken PR', NULL, N'7', N'1', 28),
	(N'WEBS2', 1516, N'HW', NULL, N'7', N'1', 70),
	(N'WEBS2', 1516, N'WS', 2, N'7', N'1', 14),
	(N'WEBS3', 1516, N'HW', NULL, N'6', N'1', 63),
	(N'WEBS3', 1516, N'TH', 1, N'7', N'1', 7),
	(N'WEBS3', 1516, N'WS', 2, N'7', N'1', 14),
	(N'WEBS4', 1516, N'HW', NULL, N'4', N'1', 24),
	(N'WEBS4', 1516, N'Tentamen', NULL, N'1', N'1', 20),
	(N'WEBS4', 1516, N'WS', 3, N'4', N'1', 12)
) AS Source ([CursusCode], [Schooljaar], [Activiteit], [ContactUren], [Duur], [Frequentie], [SBU])  
ON Target.[CursusCode] = Source.[CursusCode]
	AND Target.[Schooljaar] = Source.[Schooljaar]
	AND Target.[Activiteit] = Source.[Activiteit]
WHEN NOT MATCHED BY TARGET THEN  
	INSERT ([CursusCode], [Schooljaar], [Activiteit], [ContactUren], [Duur], [Frequentie], [SBU]) VALUES ([CursusCode], [Schooljaar], [Activiteit], [ContactUren], [Duur], [Frequentie], [SBU])
WHEN MATCHED THEN
	UPDATE SET 
		[ContactUren] = Source.[ContactUren],
		[Duur] = Source.[Duur],
		[Frequentie] = Source.[Frequentie],
		[SBU] = Source.[SBU];