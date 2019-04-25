CREATE OR ALTER PROCEDURE Employees.RetrieveJobs
AS

SELECT J.JobTitleID, J.JobName, J.Salary
FROM Employee.Jobs J
GO
