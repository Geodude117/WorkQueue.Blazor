﻿CREATE PROCEDURE [dbo].[DomainGroup_Select_All]
AS
	SELECT 
		[dg].Id,
		[dg].GroupName,
		[dg].ExternalReferenceId,
		[dg].IsActive
	FROM
		[dbo].[DomainGroup] dg
RETURN 0