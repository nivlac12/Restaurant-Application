CREATE OR ALTER PROCEDURE Inventory.GetStockItem
   @RestaurantName NVARCHAR(128),
   @FoodName NVARCHAR(128)
AS

SELECT SI.InventoryID, SI.FoodID, SI.RestaurantID, SI.Quantity
FROM Inventory.StockItems SI
	INNER JOIN Restaurants.Restaurant R ON SI.RestaurantID = R.RestaurantID
	INNER JOIN Food.Food F ON F.FoodID = SI.FoodID
WHERE R.RestaurantName = @RestaurantName
	AND F.FoodName = @FoodName;
GO

CREATE OR ALTER PROCEDURE Inventory.GetStockItemByID
   @itemID INT
AS

SELECT SI.FoodID, SI.RestaurantID, SI.Quantity
FROM Inventory.StockItems SI
WHERE SI.InventoryID = @itemID
GO