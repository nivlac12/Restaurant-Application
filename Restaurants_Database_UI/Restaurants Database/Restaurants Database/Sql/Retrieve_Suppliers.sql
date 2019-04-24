CREATE OR ALTER PROCEDURE Suppliers.RetrieveSuppliers
AS

SELECT S.SupplierID, S.[Name]
FROM Supplier.Suppliers S
GO