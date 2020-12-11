
CREATE PROCEDURE [workqueue].[QueueItem_Delete_WescotRef]
	@WescotRef int
AS
	DELETE
		workqueue.QueueItem
	FROM
		workqueue.QueueItem
	WHERE
		WescotRef = @WescotRef
RETURN 0