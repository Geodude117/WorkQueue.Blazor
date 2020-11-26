CREATE PROCEDURE [dbo].[DomainType_Select_All]
AS
	SELECT 
		[dt].[Id],
		[dt].[TypeName],
		[dt].[ClassObject]
	FROM
		[dbo].[DomainType] dt
RETURN 0