CREATE OR ALTER PROCEDURE Employees.GetJob
   @JobTitleID INT
AS

SELECT  J.JobName, J.Salary
FROM Employee.Jobs J
WHERE J.JobTitleID = @JobTitleID;
GO