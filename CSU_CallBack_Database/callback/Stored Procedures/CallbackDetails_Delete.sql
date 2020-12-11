CREATE PROCEDURE [callback].[CallbackDetails_Delete]
	@QueueItemID int
AS
	DELETE
		callback.CallbackDetail
	FROM 
		callback.CallbackDetail
	wHERE 
		QueueItemID = @QueueItemID
RETURN 0
