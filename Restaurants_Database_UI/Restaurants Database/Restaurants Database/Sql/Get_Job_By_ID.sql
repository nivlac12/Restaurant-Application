CREATE OR ALTER PROCEDURE Employees.GetJobByID
   @JobID INT
AS

SELECT  J.JobName, J.Salary
FROM Employee.Jobs J
WHERE J.JobTitleID = @JobID;
GO