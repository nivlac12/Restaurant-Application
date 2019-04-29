CREATE OR ALTER PROCEDURE Food.RetrieveFood
AS

SELECT f.FoodID, F.SupplierID, F.[FoodName], F.SupplierPrice, F.RetailPrice
FROM Food.Food F;
GO