CREATE PROCEDURE [dbo].[DomainInformation_Select_All]
	AS
	SELECT 
		[di].[Id],
		[di].[Title],
		[di].[ObjectMapping],
		[di].[Order],
		[di].[GroupId],
		[di].[Arguments],
		[di].[HasValidation]

	FROM
		[dbo].[DomainInformation] di
RETURN 0