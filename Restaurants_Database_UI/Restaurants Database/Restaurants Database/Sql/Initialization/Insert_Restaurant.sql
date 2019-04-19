CREATE OR ALTER PROCEDURE Restaurants.CreateRestaurant
   @OrganizationID INT,
   @RestaurantName NVARCHAR(64),
   @IsOperational BIT,
   @RestaurantID INT OUTPUT
AS

INSERT Restaurants.Restaurant(OrganizationID, RestaurantName, DateFounded, IsOperational)
VALUES(@OrganizationID, @RestaurantName, SYSDATETIMEOFFSET(), @IsOperational);

SET @RestaurantID = SCOPE_IDENTITY();
GO
