CREATE PROCEDURE [callback].[CallbackDetail_Select_By_QueueItemID]
	@QueueItemID int 
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
		cbd.QueueItemID = @QueueItemID
RETURN 0
