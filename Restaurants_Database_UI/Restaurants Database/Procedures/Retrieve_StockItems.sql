CREATE OR ALTER PROCEDURE Inventory.RetrieveStockItems
AS

SELECT SI.InventoryID, SI.FoodID, SI.RestaurantID, SI.Quantity
FROM Inventory.StockItems SI
GO