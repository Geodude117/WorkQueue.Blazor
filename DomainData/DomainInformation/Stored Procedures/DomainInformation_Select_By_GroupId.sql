CREATE PROCEDURE [dbo].[DomainInformation_Select_By_GroupId]
	@GroupId int
AS
	SELECT 
		[di].[Id],
		[di].[Title],
		[di].[PropertyMapping],
		[di].[Order],
		[di].[GroupId],
		[di].[TypeId],
		[di].[Arguments],
		[di].[HasValidation]
	FROM
		[dbo].[DomainInformation] di
	where
		[di].[GroupId] = @GroupId
RETURN 0
