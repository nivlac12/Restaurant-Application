CREATE OR ALTER PROCEDURE Restaurants.CreateRestaurant
   @OrganizationID INT,
   @RestaurantName NVARCHAR(64),
   @IsOperational BIT,
   @RestaurantID INT OUTPUT,
   @DateFounded DATETIMEOFFSET OUTPUT
AS

SET @DateFounded = SYSDATETIMEOFFSET();
INSERT Restaurants.Restaurant(OrganizationID, RestaurantName, DateFounded, IsOperational)
VALUES(@OrganizationID, @RestaurantName, @DateFounded, @IsOperational);

SET @RestaurantID = SCOPE_IDENTITY();
GO
