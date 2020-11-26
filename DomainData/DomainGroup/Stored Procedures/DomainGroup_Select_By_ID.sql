CREATE PROCEDURE [dbo].[DomainGroup_Select_By_Id]
	@Id int
AS
	SELECT 
		[dg].Id,
		[dg].GroupName,
		[dg].ExternalReferenceId,
		[dg].IsActive
	FROM
		[dbo].[DomainGroup] dg
	where 
		[dg].Id = @Id
RETURN 0
