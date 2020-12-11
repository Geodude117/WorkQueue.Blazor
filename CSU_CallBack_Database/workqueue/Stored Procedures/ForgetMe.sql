

CREATE PROCEDURE [workqueue].[ForgetMe]
	@WescotRef int
AS

	EXEC [callback].[CallbackDetails_Delete_WescotRef] @WescotRef
	EXEC [workqueue].[QueueItem_Delete_WescotRef] @WescotRef

RETURN 0