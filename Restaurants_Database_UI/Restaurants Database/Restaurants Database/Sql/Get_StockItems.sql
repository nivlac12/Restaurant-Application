CREATE OR ALTER PROCEDURE Inventory.GetStockItem
   @InventoryName NVARCHAR(128)
AS

SELECT SI.InventoryID, SI.FoodID, SI.RestaurantID, SI.Quantity
FROM Inventory.StockItems SI
WHERE SI.InventoryName = @InventoryName;
GO