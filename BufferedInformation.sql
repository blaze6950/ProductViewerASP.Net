INSERT Production.ProductInventory (ProductID, LocationID, Shelf, Bin, Quantity)
VALUES (1000, '1', 'A', 56, 77)

SELECT * FROM Production.ProductInventory WHERE LocationID = 1

EXECUTE dbo.InsertProductInventory

DELETE FROM Production.ProductInventory WHERE ProductID = 1000

--E:\GoogleDrive\Программовня\Work\

INSERT INTO Production.ProductListPriceHistory (ProductID, EndDate, StartDate, ListPrice)
VALUES (1000, 'NULL', @StartDate, @ListPrice)