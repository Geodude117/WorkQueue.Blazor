CREATE PROCEDURE [dbo].[DomainGroup_Select_By_Is_Active]
	@IsActive varchar(100)
AS
	SELECT 
		[dg].Id,
		[dg].GroupName,
		[dg].ClassMapping,
		[dg].ExternalReferenceId,
		[dg].IsActive
	FROM
		[dbo].[DomainGroup] dg
	where 
		[dg].IsActive = @IsActive
RETURN 0
