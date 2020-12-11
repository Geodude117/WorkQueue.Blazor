CREATE PROCEDURE [workqueue].[QueueItem_Insert]
	@WescotRef int,
	@CustomerName nvarchar(100),
	@CompletedDate datetime,
	@CompletedBy nvarchar(100),
	@DueDate datetime,
	@CreatedDate datetime,
	@CreatedBy nvarchar(100),
	@Summary nvarchar(500),
	@ParentQueueItemID int,
	@QueueID int
AS
	INSERT INTO	workqueue.QueueItem 
		(
			WescotRef,
			CustomerName,
			CompletedDate,
			CompletedBy,
			DueDate,
			CreatedDate,
			CreatedBy,
			Summary,
			ParentQueueItemID,
			QueueID
		)
	VALUES 
		(
			@WescotRef,
			@CustomerName,
			@CompletedDate,
			@CompletedBy,
			@DueDate,
			@CreatedDate,
			@CreatedBy,
			@Summary,
			@ParentQueueItemID,
			@QueueID
		)

RETURN SCOPE_IDENTITY()
