
CREATE PROCEDURE [callback].[CallbackDetails_Delete_WescotRef]
	@WescotRef int
AS
	DELETE
		callback.CallbackDetail
	FROM 
		callback.CallbackDetail
	WHERE 
		WescotRef = @WescotRef
RETURN 0