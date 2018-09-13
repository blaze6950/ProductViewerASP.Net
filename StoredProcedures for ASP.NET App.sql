-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE InsertProduct 
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50),
    @ProductNumber int,
	@SafetyStockLevel smallint,
	@ReorderPoint smallint,
	@StandardCost money,
	@ListPrice money,
	@DaysToManufacture int,
	@SellStartDate datetime,
	@ProductModelID int,
    @Id int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Production.Product
    (Name, ProductNumber, SafetyStockLevel,
    ReorderPoint, StandardCost, ListPrice,
    DaysToManufacture, SellStartDate, ProductModelID, ViewStatus)
    VALUES (@Name, @ProductNumber, @SafetyStockLevel,
    @ReorderPoint, @StandardCost, @ListPrice,
    @DaysToManufacture, @SellStartDate, @ProductModelID, 1)
    SET @Id = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE InsertProductDescription
	-- Add the parameters for the stored procedure here
	@Description nvarchar(400),
    @Id int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Production.ProductDescription (Description)
    VALUES (@Description)
    SET @Id = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE InsertProductModel
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50),
    @Id int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Production.ProductModel (Name)
    VALUES (@Name)
    SET @Id = SCOPE_IDENTITY()
END
GO
