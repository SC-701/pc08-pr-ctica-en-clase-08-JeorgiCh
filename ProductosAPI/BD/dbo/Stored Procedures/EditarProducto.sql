
CREATE PROCEDURE EditarProducto
	@Id AS uniqueIdentifier
	,@IdSubCategoria as uniqueidentifier
	,@Nombre AS varchar(100) 
   ,@Descripcion AS varchar(500) 
   ,@Precio AS decimal(18,2) 
   ,@Stock AS int 
   ,@CodigoBarras AS varchar(max) 
AS
BEGIN
	SET NOCOUNT ON;

    BEGIN TRANSACTION
	UPDATE [dbo].[Producto]
   SET
      [Nombre] = @Nombre
      ,[Descripcion] = @Descripcion
      ,[Precio] = @Precio
      ,[Stock] = @Stock
      ,[CodigoBarras] = @CodigoBarras
	  ,[IdSubCategoria] = @IdSubCategoria
 WHERE Id = @Id

 SELECT @Id
 COMMIT TRANSACTION
END