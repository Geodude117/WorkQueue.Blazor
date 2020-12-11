CREATE PROCEDURE [callback].[CallbackDetail_Select_By_WescotRef]
	@WescotRef nvarchar(50)
AS
	SELECT
		[cbd].[ID],
		[cbd].[WescotRef],
		[cbd].[DateForCallback],
		[cbd].[NameOfCaller],
		[cbd].[Relationship],
		[cbd].[ContactNumber],
		[cbd].[TimeToAvoid],
		[cbd].[ReasonForCallback],
		[cbd].[ReasonForTransfer],
		[cbd].[HealthIssue],
		[cbd].[QueueItemID]
	FROM 
		callback.CallbackDetail cbd
	WHERE 
		cbd.WescotRef = @WescotRef
RETURN 0
