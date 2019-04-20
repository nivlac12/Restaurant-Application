CREATE OR ALTER PROCEDURE Employees.CreateJob
   @JobName NVARCHAR(64),
   @Salary DECIMAL,
   @JobTitleID INT OUTPUT
AS

INSERT Employees.Jobs(JobName, Salary)
VALUES(@JobName, @Salary);

SET @JobTitleID = SCOPE_IDENTITY();
GO
