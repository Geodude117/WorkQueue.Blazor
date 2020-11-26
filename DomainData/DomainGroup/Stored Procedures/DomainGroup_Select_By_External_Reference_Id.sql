CREATE PROCEDURE [dbo].[DomainGroup_Select_By_External_Reference_Id]
	@ReferenceId varchar(100)
AS
	SELECT 
		[dg].Id,
		[dg].GroupName,
		[dg].ExternalReferenceId,
		[dg].IsActive
	FROM
		[dbo].[DomainGroup] dg
	where 
		[dg].ExternalReferenceId = @ReferenceId
RETURN 0
