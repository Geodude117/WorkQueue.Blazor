CREATE PROCEDURE [workqueue].[Queue_Select_All]
	
AS
	SELECT
		[q].[QueueID],
		[q].[QueueGroupID],
		[q].[Name],
		[q].[IsActive],
		[q].[RAGRuleID],
		[q].[DefaultStatus]
	FROM
		workqueue.Queue q
RETURN 0
