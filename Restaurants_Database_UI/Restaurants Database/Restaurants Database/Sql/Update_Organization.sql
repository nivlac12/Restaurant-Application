CREATE OR ALTER PROCEDURE Restaurants.GetOrganization
	@OrganizationID INT,
	@OrganizationName NVARCHAR(128),
	@DateFounded DATETIMEOFFSET 
AS

UPDATE Restaurants.Organization
SET
	OrganizationName = @OrganizationName,
	DateFounded = @DateFounded
WHERE OrganizationID = @OrganizationID