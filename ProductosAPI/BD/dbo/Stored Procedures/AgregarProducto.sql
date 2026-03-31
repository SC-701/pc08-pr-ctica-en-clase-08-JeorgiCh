
CREATE PROCEDURE AgregarProducto
	@Id AS uniqueIdentifier
	,@Nombre AS varchar(100)
   ,@Descripcion AS varchar(500)
   ,@Precio AS decimal(18,2)
   ,@Stock AS int
   ,@CodigoBarras AS varchar(max)
	,@IdSubCategoria AS uniqueIdentifier 
AS
BEGIN
	SET NOCOUNT ON;

    BEGIN TRANSACTION
	INSERT INTO [dbo].[Producto]
           ([Id]
           ,[Nombre]
           ,[Descripcion]
           ,[Precio]
           ,[Stock]
           ,[CodigoBarras]
			,[IdSubCategoria])
     VALUES
           (@Id
           ,@Nombre
           ,@Descripcion
           ,@Precio
           ,@Stock
           ,@CodigoBarras
			,@IdSubCategoria)
    SELECT @Id
    COMMIT TRANSACTION
END