CREATE OR ALTER PROCEDURE Restaurants.CreateRestaurant
   @Name NVARCHAR(64),
AS

INSERT Supplier.Suppliers(Name)
VALUES(@Name);

SET @SupplierID = SCOPE_IDENTITY();
GO
