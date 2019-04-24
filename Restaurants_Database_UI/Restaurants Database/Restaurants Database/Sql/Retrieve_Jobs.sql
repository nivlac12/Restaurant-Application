CREATE OR ALTER PROCEDURE Employee.RetrieveJobs
AS

SELECT J.JobTitleID, J.JobName, J.Salary
FROM Employee.Jobs J
GO
