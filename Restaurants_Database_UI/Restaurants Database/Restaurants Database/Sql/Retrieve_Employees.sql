CREATE OR ALTER PROCEDURE Employee.RetrieveEmployees
AS

SELECT E.PersonID, E.RestaurantID, E.JobTitleID, E.[Name], E.Seniority
FROM Employees.Employee E
GO
