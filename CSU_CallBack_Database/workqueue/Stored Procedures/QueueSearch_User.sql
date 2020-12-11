

CREATE PROCEDURE [workqueue].[QueueSearch_User]
	@QueueItemId int = 0
AS
Declare @locker nvarchar(100), @Id int

	SELECT
		@locker = [qi].[LockedBy]
	FROM
		workqueue.QueueItem qi
	WHERE
		qi.QueueItemId = @QueueItemId

		select @locker


update 
	workqueue.QueueItem
set
	LockedBy = null,
	LockTime = null
where
	LockedBy = @locker
and QueueItemId != @QueueItemId

RETURN 0