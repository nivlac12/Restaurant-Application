CREATE OR ALTER PROCEDURE Employees.UpdateJob
	@JobTitleID INT,
    @JobName NVARCHAR(64),
	@Salary FLOAT
AS

UPDATE Employees.Jobs
SET
	JobName = @JobName,
	Salary = @Salary
WHERE JobTitleID = @JobTitleID