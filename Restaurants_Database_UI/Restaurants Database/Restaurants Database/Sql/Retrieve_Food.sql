CREATE OR ALTER PROCEDURE Food.RetrieveFood
AS

SELECT F.SupplierID, F.[FoodName], F.SupplierPrice, F.RetailPrice
FROM Food.Food F;
GO