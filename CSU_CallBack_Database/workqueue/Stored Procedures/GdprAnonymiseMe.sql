
CREATE PROCEDURE [workqueue].[GdprAnonymiseMe]
	@WescotRef int
AS

	EXEC [callback].[GdprAnonymiseMe] @WescotRef
	EXEC [workqueue].[QueueItemGdprAnnoymiseMe] @WescotRef

RETURN 0