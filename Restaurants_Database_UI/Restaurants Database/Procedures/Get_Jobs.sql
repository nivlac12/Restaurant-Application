CREATE OR ALTER PROCEDURE Employees.GetJob
   @JobName NVARCHAR(128)
AS

SELECT  J.JobTitleID, J.Salary
FROM Employees.Jobs J
WHERE J.JobName = @JobName;
GO