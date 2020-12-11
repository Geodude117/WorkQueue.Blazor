CREATE PROCEDURE [workqueue].[QueueAction_Select_All]
AS

	SELECT 
		[qa].[QueueActionID],
		[qa].[QueueResultID],
		[qa].[ActionID],
		[qa].[ActionCode],
		[qa].[DefaultDays]
	FROM
		workqueue.QueueAction qa
	ORDER BY 
		qa.ActionCode
RETURN 0
