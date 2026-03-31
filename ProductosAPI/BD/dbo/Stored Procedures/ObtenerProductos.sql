
CREATE PROCEDURE ObtenerProductos
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		P.[Id],
		P.[Nombre],
		P.[Descripcion],
		P.[Precio],
		P.[Stock],
		P.[CodigoBarras],
		P.[IdSubCategoria],
		S.[Nombre] AS SubCategorias, 
		C.[Nombre] AS Categorias    
	FROM [dbo].[Producto] AS P
	INNER JOIN [dbo].[SubCategorias] AS S ON P.IdSubCategoria = S.Id
	INNER JOIN [dbo].[Categorias] AS C ON S.IdCategoria = C.Id
END