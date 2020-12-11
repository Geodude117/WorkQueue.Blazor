CREATE PROCEDURE [workqueue].[QueueItem_Select_Search]
	@StartDate DATETIME,
	@EndDate DATETIME,
	@WescotRef INT,
	@RaiseAgentId NVARCHAR(100),
	@ActionAgentId NVARCHAR(100),
	@QueueGroupId INT,
	@QueueItemID INT, 
	@IsActive bit
AS
	SELECT
		[qi].[QueueItemID], [qi].[WescotRef], [qi].[CustomerName], [qi].[CompletedDate], [qi].[CompletedBy], [qi].[DueDate], [qi].[CreatedDate], [qi].[CreatedBy], [qi].[Summary], [qi].[ParentQueueItemID],[qi].[LockTime], [qi].[Islocked], [qi].[LockedBy] , [qi].[QueueID],[qi].QueueGroupID, [qi].[RAGRuleID], [qi].[RAGStatus], [qi].[LowValue], [qi].[MidValue], [qi].[HighValue], [qi].[LowToHigh]
		
	FROM
		workqueue.QueueItemWithRAG qi
	WHERE
		(@QueueGroupId IS NULL OR qi.QueueGroupID = @QueueGroupId)
	AND (@RaiseAgentId IS NULL OR qi.CreatedBy = @RaiseAgentId)
	AND (@ActionAgentId IS NULL OR qi.CompletedBy = @ActionAgentId)
	and (@WescotRef is null or qi.WescotRef = @WescotRef)
	and (@StartDate is null or qi.CreatedDate >= @StartDate)
	and (@EndDate is null or qi.CompletedDate <= @EndDate)
	and (@QueueItemID is null or qi.QueueItemID = @QueueItemID)
	and (@IsActive is null 
			or @IsActive = 1 and qi.CompletedDate is null 
			or @IsActive = 0 and qi.CompletedDate is not null)

