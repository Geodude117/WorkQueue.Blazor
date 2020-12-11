CREATE PROCEDURE [workqueue].[QueueResult_Search_Action_Code_By_QueueResultID]
	@QueueResultID int
AS
	SELECT 
			[qa].[ActionID], 
			[qa].[ActionCode],
			[qa].[DefaultDays]
	FROM
		workqueue.QueueAction qa
	WHERE 
		[qa].[QueueResultID] = @QueueResultID
