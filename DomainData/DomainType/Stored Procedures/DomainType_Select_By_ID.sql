CREATE PROCEDURE [dbo].[DomainType_Select_By_ID]
	@Id int
AS
	SELECT 
		[dt].[Id],
		[dt].[TypeName],
		[dt].[ClassObject]
	FROM
		[dbo].[DomainType] dt
	WHERE
		dt.Id = @Id
RETURN 0
