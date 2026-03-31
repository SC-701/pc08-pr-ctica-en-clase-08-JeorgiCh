
CREATE PROCEDURE ObtenerProducto
	@Id uniqueIdentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		P.Id, 
		P.Nombre, 
		P.Descripcion, 
		P.Precio, 
		P.Stock, 
		P.CodigoBarras,
		P.IdSubCategoria,
		S.Nombre AS SubCategorias, 
		C.Nombre AS Categorias    
	FROM Producto P
	INNER JOIN SubCategorias S ON P.IdSubCategoria = S.Id
	INNER JOIN Categorias C ON S.IdCategoria = C.Id
	WHERE P.Id = @Id
END