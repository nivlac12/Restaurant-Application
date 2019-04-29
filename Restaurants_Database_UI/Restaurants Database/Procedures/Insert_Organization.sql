CREATE OR ALTER PROCEDURE Restaurants.CreateOrganization
   @OrganizationName NVARCHAR(128),
   @DateFounded DATETIMEOFFSET OUTPUT,
   @OrganizationID INT OUTPUT
AS
SET @DateFounded = SYSDATETIMEOFFSET();
INSERT Restaurants.Organization(OrganizationName, DateFounded)
VALUES(@OrganizationName, @DateFounded);

SET @OrganizationID = SCOPE_IDENTITY();
GO
