CREATE OR ALTER PROCEDURE Employees.GetEmployee
   @PersonID INT
AS

SELECT E.RestaurantID, E.JobTitleID, E.[Name], E.Seniority
FROM Employees.Employee E
WHERE E.PersonID = @PersonID;
GO