CREATE OR ALTER PROCEDURE Restaurants.UpdateRestaurant
	@RestaurantID INT,
	@OrganizationID INT,
	@RestaurantName NVARCHAR(128),
	@IsOperational BIT

AS

UPDATE Restaurants.Restaurant
SET
	OrganizationID = @OrganizationID,
	RestaurantName = @RestaurantName,
	IsOperational = @IsOperational
WHERE RestaurantID = @RestaurantID


