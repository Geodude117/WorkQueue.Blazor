CREATE PROCEDURE [workqueue].[Queue_Get]
	@QId int = null
AS
	Select [q].[QueueID], [q].[QueueGroupID], [q].[Name], [q].[IsActive], [q].[RagRuleID], [q].[DefaultStatus] 
	from 
		[WorkQueues].[workqueue].[Queue] q
	where 
		(@QId IS NULL OR q.QueueID = @QId)


RETURN 0
