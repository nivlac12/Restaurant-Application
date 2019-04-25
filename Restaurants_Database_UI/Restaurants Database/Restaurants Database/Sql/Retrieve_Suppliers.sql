CREATE OR ALTER PROCEDURE Supplier.RetrieveSuppliers
AS

SELECT S.SupplierID, S.[Name]
FROM Supplier.Suppliers S
GO