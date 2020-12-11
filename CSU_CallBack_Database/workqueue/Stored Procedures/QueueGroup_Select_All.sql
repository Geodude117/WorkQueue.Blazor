CREATE PROCEDURE [workqueue].[QueueGroup_Select_All]
AS

	SELECT
		[qg].[QueueGroupID],
		[qg].[Name],
		[qg].[IsActive],
		[qg].[AccessGroupPublic],
		[qg].[AccessGroupBase],
		[qg].[AccessGroupExtended],
		[qg].[AccessGroupAdmin],
		[qg].[DefaultQueueID]
	FROM
		workqueue.QueueGroup qg
	ORDER BY 
		qg.QueueGroupID ASC
RETURN 0
