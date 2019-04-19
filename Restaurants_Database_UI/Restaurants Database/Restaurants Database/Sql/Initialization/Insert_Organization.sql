CREATE OR ALTER PROCEDURE Restaurants.CreateRestaurant
   @OrganizationName NVARCHAR(128),
   @DateFounded DATETIMEOFFSET
AS

INSERT Restaurants.Organization(OrganizationName, DateFounded)
VALUES(@OrganizationName, SYSDATETIMEOFFSET());

SET @OrganizationID = SCOPE_IDENTITY();
GO
