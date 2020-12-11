CREATE PROCEDURE [workqueue].[QueueResult_Select_QueueResult_ByQueueID]
	@QueueID int
AS
	SELECT 
		[qr].[QueueResultID], [qr].[QueueResult], [qr].[QueueID]
	FROM 
		workqueue.QueueResult qr
	WHERE
		qr.QueueID = @QueueID 

RETURN 0
