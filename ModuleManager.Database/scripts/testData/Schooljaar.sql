﻿MERGE INTO Schooljaar AS Target  
USING (values 
	(1415), 
	(1516)
) AS Source (JaarId)  
ON Target.JaarId = Source.JaarId  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (JaarId) VALUES (JaarId);