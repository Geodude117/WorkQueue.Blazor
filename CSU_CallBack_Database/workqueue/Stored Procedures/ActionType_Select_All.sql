CREATE PROCEDURE [workqueue].[ActionType_Select_All]
AS

	select
		[act].[ActionID], 
		[act].[ActionDescription]
	FROM
		workqueue.ActionType act
	ORDER BY
		act.ActionDescription 

RETURN 0
