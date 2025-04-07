# FinBeat
тестовое задание для бэка.

Задание 2.

1)
 SELECT c."ClientName" AS ClientName,
       COUNT(cc."Id") AS ContactCount
 FROM "Clients" c
 LEFT JOIN "ClientContacts" as cc
    ON c."Id" = cc."ClientId"
 GROUP BY c."ClientName"

2)
 SELECT c.Id, c.ClientName, COUNT(cc."Id")
 FROM Clients c
 INNER JOIN ClientContacts cc
    ON c.Id = cc.ClientId
 GROUP BY c.Id, c.ClientName
 HAVING COUNT(cc.Id) > 2



Задание 3.

SELECT 
    ClientId,
    StartDate,
    EndDate
FROM 
    (
		SELECT 
        "Id" AS ClientId,
        "Dt" AS StartDate,
        LEAD("Dt") OVER (PARTITION BY "Id" ORDER BY "Dt") AS EndDate,
        ROW_NUMBER() OVER (ORDER BY "Id", "Dt") AS rn
    	FROM "Dates"
	) intervals
WHERE EndDate IS NOT NULL
ORDER BY 
    ClientId, StartDate


