CREATE PROCEDURE [callback].[CallbackDetail_Insert]
	@WescotRef int,
	@DateForCallback datetime,
	@NameOfCaller nvarchar(100),
	@Relationship  nvarchar(100),
	@ContactNumber nvarchar(50),
	@TimeToAvoid nvarchar(200),
	@ReasonForCallback nvarchar(4000),
	@ReasonForTransfer  nvarchar(4000),
	@HealthIssue nvarchar(4000),
	@QueueItemID int
AS
	INSERT INTO	callback.CallbackDetail 
		(
			WescotRef,
			DateForCallback,
			NameOfCaller,
			Relationship,
			ContactNumber,
			TimeToAvoid,
			ReasonForCallback,
			ReasonForTransfer,
			HealthIssue,
			QueueItemID
		)	
	VALUES 
		(
			@WescotRef,
			@DateForCallback,
			@NameOfCaller,
			@Relationship,
			@ContactNumber,
			@TimeToAvoid,
			@ReasonForCallback,
			@ReasonForTransfer,
			@HealthIssue,
			@QueueItemID
		)


RETURN SCOPE_IDENTITY()
