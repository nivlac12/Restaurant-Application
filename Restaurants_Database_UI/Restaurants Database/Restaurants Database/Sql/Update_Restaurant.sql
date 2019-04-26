CREATE OR ALTER PROCEDURE Restaurants.UpdateRestaurant
	@RestaurantID INT,
	@OrganizationID INT,
	@RestaurantName NVARCHAR(128),
	@DateFounded DATETIMEOFFSET,
	@IsOperational BIT

AS

UPDATE Restaurants.Restaurant
SET
	OrganizationID = @OrganizationID,
	RestaurantName = @RestaurantName,
	DateFounded = @DateFounded,
	IsOperational = @IsOperational
WHERE RestaurantID = @RestaurantID


