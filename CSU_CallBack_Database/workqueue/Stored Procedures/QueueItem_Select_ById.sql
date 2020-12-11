CREATE PROCEDURE [workqueue].[QueueItem_Select_ById]
	@queueItemId int
AS

	SELECT
		[qi].[QueueItemID], [qi].[WescotRef], [qi].[CustomerName], [qi].[CompletedDate], [qi].[CompletedBy], [qi].[DueDate], [qi].[CreatedDate], [qi].[CreatedBy], [qi].[Summary], [qi].[ParentQueueItemID], [qi].[LockTime], [qi].[Islocked], [qi].[LockedBy], [qi].[QueueID], [qi].[QueueGroupID], [qi].[RAGRuleID], [qi].[RAGStatus], [qi].[LowValue], [qi].[MidValue], [qi].[HighValue], [qi].[LowToHigh]
	FROM
		workqueue.QueueItemWithRAG qi
	where
		qi.QueueItemID = @queueItemId


