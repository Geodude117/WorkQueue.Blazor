
CREATE PROCEDURE [workqueue].[QueueItemGdprAnnoymiseMe]
	@WescotRef int
AS

	UPDATE
		workqueue.QueueItem
	SET
		CustomerName = 'Wes Cot',
		Summary = '6a 61 63 6b 20 77 61 73 20 68 65 72 65 '
	WHERE
		WescotRef = @WescotRef

RETURN 0