CREATE PROCEDURE [workqueue].[QueueItem_Update_By_ID]
	@QueueItemID int,
	@WescotRef int,
	@CustomerName nvarchar(100),
	@CompletedDate datetime,
	@CompletedBy nvarchar(100),
	@DueDate datetime,
	@CreatedDate datetime,
	@CreatedBy nvarchar(100),
	@Summary nvarchar(500),
	@ParentQueueItemID int,
	@LockTime datetime,
	@LockedBy nvarchar(100)
AS

	UPDATE
		workqueue.QueueItem
	SET
		WescotRef = @WescotRef,
		CustomerName = @CustomerName,
		CompletedDate = @CompletedDate,
		CompletedBy = @CompletedBy,
		DueDate = @DueDate,
		CreatedDate = @CreatedDate,
		CreatedBy = @CreatedBy,
		Summary = @Summary,
		ParentQueueItemID = @ParentQueueItemID,
		LockTime = @LockTime,
		LockedBy = @LockedBy
	WHERE
		QueueItemID = @QueueItemID

	EXEC [workqueue].[QueueSearch_User] @QueueItemId
RETURN 0
