CREATE OR ALTER PROCEDURE Inventory.CreateStockItem
   @FoodID INT,
   @RestaurantID INT,
   @Quantity INT,
   @InventoryID INT OUTPUT
AS

INSERT Inventory.StockItems(FoodID, RestaurantID, Quanitity)
VALUES(@FoodID, @RestaurantID, @Quantity);

SET @InvetoryID = SCOPE_IDENTITY();
GO
