CREATE PROCEDURE [workqueue].[QueueItem_Delete]
	@QueueItemID int
AS
	DELETE
		workqueue.QueueItem
	FROM
		workqueue.QueueItem
	WHERE
		QueueItemID = @QueueItemID
RETURN 0
