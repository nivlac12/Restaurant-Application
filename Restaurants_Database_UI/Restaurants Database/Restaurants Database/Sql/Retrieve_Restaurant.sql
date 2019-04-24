CREATE OR ALTER PROCEDURE Restaurants.RetrieveRestaurant
AS

SELECT R.RestaurantID, R.OrganizationID, R.RestaurantName, R.DateFounded, R.IsOperational
FROM Restaurants.Restaurant R
GO