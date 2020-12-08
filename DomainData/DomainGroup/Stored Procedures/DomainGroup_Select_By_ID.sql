CREATE PROCEDURE [dbo].[DomainGroup_Select_By_Id]
	@Id int
AS
	SELECT 
		[dg].Id,
		[dg].GroupName,
		[dg].ClassMapping,
		[dg].ExternalReferenceId,
		[dg].IsActive,
		[dg].[AccessGroupPublic],
		[dg].[AccessGroupBase],
		[dg].[AccessGroupExtended],
		[dg].[AccessGroupAdmin]
	FROM
		[dbo].[DomainGroup] dg
	where 
		[dg].Id = @Id
RETURN 0
