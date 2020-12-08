CREATE PROCEDURE [dbo].[DomainGroup_Select_By_External_Reference_Id]
	@ReferenceId varchar(100)
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
		[dg].ExternalReferenceId = @ReferenceId
RETURN 0
