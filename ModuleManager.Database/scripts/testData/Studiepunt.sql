MERGE INTO Studiepunt AS Target  
USING (values 
	(N'DATAB1', 1516, N'DATAB1-TH', N'Tentamen', 3, N'5,5'),
	(N'DATAB2', 1516, N'DATAB2-TP', N'Tentamen (PC)', 3, N'5,5'),
	(N'ICTIB', 1516, N'ICTIB-PR', N'Opdracht(en)', 2, N'5,5'),
	(N'INPRJ2', 1516, N'INPRJ2-IN', N'Individueel assessment', 1, N'v'),
	(N'INPRJ2', 1516, N'INPRJ2-PR', N'Groepsassessment', 3, N'5,5'),
	(N'INTRVW', 1516, N'INTERVW-PR', N'Opdracht(en)', 0.5, N'vld'),
	(N'INTRVW', 1516, N'INTRVW-TH', N'Tentamen', 0.5, N'v'),
	(N'MODL1', 1516, N'MODL1-TP', N'Tentamen', 2, N'5,5'),
	(N'ORI1', 1516, N'ORI1-GR', N'Opdracht(en)', 2, N'vld'),
	(N'PROG1', 1516, N'PROG1-PR', N'Tentamen (PC)', 4, N'5,5'),
	(N'PROG2', 1516, N'PROG2-PR', N'Individueel assessment', 4, N'5,5'),
	(N'SLC01', 1516, N'SLC01-PR', N'Opdracht(en)', 2, N'vld'),
	(N'VERGA', 1516, N'VERGA-PR', N'Opdracht(en)', 1, N'vld'),
	(N'WEBS1', 1516, N'WEBS1-PR', N'Opdracht(en)', 3, N'5,5')
) AS Source ([CursusCode], [Schooljaar], [ToetsCode], [Toetsvorm], [EC], [Minimum])  
ON Target.[CursusCode] = Source.[CursusCode]
	AND Target.[Schooljaar] = Source.[Schooljaar]
	AND Target.[ToetsCode] = Source.[ToetsCode]
WHEN NOT MATCHED BY TARGET THEN  
	INSERT ([CursusCode], [Schooljaar], [ToetsCode], [Toetsvorm], [EC], [Minimum]) VALUES ([CursusCode], [Schooljaar], [ToetsCode], [Toetsvorm], [EC], [Minimum])
WHEN MATCHED THEN
	UPDATE SET
		[ToetsCode] = Source.[ToetsCode],
		[Toetsvorm] = Source.[Toetsvorm],
		[EC] = Source.[EC],
		[Minimum] = Source.[Minimum];