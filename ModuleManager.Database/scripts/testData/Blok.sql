MERGE INTO Blok AS Target  
USING (values 
	(1),
	(2),
	(3),
	(4),
	(5),
	(6),
	(7),
	(8),
	(9),
	(10),
	(11),
	(12),
	(13),
	(14),
	(15),
	(16)
) AS Source (BlokId)  
ON Target.BlokId = Source.BlokId  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (BlokId) VALUES (BlokId);