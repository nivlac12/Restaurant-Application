CREATE OR ALTER PROCEDURE Supplier.GetSupplier
   @SupplierName NVARCHAR(128)
AS

SELECT S.SupplierID
FROM Supplier.Suppliers S
WHERE S.Name = @SupplierName;
GO

CREATE OR ALTER PROCEDURE Supplier.GetSupplierByID
   @SupplierID INT
AS

SELECT S.Name
FROM Supplier.Suppliers S
WHERE S.SupplierID = @SupplierID;
GO