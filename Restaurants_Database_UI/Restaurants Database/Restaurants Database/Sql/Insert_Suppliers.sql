CREATE OR ALTER PROCEDURE Supplier.CreateSupplier
   @Name NVARCHAR(64),
   @SupplierID INT OUTPUT
AS

INSERT Supplier.Suppliers(Name)
VALUES(@Name);

SET @SupplierID = SCOPE_IDENTITY();
GO
