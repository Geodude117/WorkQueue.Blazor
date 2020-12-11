CREATE PROCEDURE [workqueue].[QueueResult_Select_All]
AS

	SELECT 
		[qr].[QueueResultID],
		[qr].[QueueResult],
		[qr].[QueueID]
	FROM 
		workqueue.QueueResult qr
	ORDEr BY
		qr.QueueResult
RETURN 0
