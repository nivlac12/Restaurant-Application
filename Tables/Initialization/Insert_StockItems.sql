CREATE OR ALTER PROCEDURE Restaurants.CreateRestaurant
   @FoodID INT,
   @RestaurantID INT,
   @Quantity INT
AS

INSERT Inventory.StockItems(FoodID, RestaurantID, Quanitity)
VALUES(@FoodID, @RestaurantID, @Quantity);

SET @InvetoryID = SCOPE_IDENTITY();
GO
