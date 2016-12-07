MERGE INTO Toetsvorm AS Target  
USING (values 
	('Groepsassessment' ), 
	('Individueel assessment' ),
	('Tentamen' ),
	('Tentamen (PC)' ),
	('Opdracht(en)' )
) AS Source ([Type])  
ON Target.[Type] = Source.[Type]
WHEN NOT MATCHED BY TARGET THEN  
	INSERT ([Type]) VALUES ([Type]);