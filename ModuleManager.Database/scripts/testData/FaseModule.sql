MERGE INTO FaseModule AS Target  
USING (values 
	(N'Regulier', N'DATAB1', 1516),
	(N'Regulier', N'DATAB2', 1516),
	(N'Regulier', N'ENUP1', 1516),
	(N'Regulier', N'ENUP2', 1516),
	(N'Regulier', N'ICTIB', 1516),
	(N'Regulier', N'INPRJ2', 1516),
	(N'Regulier', N'INTRVW', 1516),
	(N'Regulier', N'MODL1', 1516),
	(N'Regulier', N'ORI1', 1516),
	(N'Regulier', N'PROG1', 1516),
	(N'Regulier', N'PROG2', 1516),
	(N'Regulier', N'SLC01', 1516),
	(N'Regulier', N'VERGA', 1516),
	(N'Regulier', N'WEBS1', 1516)
) AS Source ([FaseNaam], [ModuleCursusCode], [ModuleSchooljaar])  
ON Target.[FaseNaam] = Source.[FaseNaam]
	AND Target.[ModuleCursusCode] = Source.[ModuleCursusCode]
	AND Target.[ModuleSchooljaar] = Source.[ModuleSchooljaar]
WHEN NOT MATCHED BY TARGET THEN  
	INSERT ([FaseNaam], [ModuleCursusCode], [ModuleSchooljaar]) VALUES (Source.[FaseNaam], Source.[ModuleCursusCode], Source.[ModuleSchooljaar]);