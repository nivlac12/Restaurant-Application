CREATE OR ALTER PROCEDURE Inventory.GetStockItems
   @InventoryID INT
AS

SELECT SI.FoodID, SI.RestaurantID, SI.Quantity
FROM Inventory.StockItems SI
WHERE SI.InventoryID = @InventoryID;
GO