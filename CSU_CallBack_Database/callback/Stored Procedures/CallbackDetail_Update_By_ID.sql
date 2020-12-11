CREATE PROCEDURE [callback].[CallbackDetail_Update_By_ID]
	@ID int,
	@WescotRef int,
	@DateForCallback datetime,
	@NameOfCaller nvarchar(100),
	@Relationship  nvarchar(100),
	@ContactNumber nvarchar(50),
	@TimeToAvoid nvarchar(200),
	@ReasonForCallback nvarchar(4000),
	@ReasonForTransfer  nvarchar(4000),
	@HealthIssue nvarchar(4000)
AS
	UPDATE 
		callback.CallbackDetail 
	SET
		WescotRef = @WescotRef,
		DateForCallback = @DateForCallback,
		NameOfCaller = @NameOfCaller,
		Relationship = @Relationship,
		ContactNumber = @ContactNumber,
		TimeToAvoid = @TimeToAvoid,
		ReasonForCallback = @ReasonForCallback,
		ReasonForTransfer = @ReasonForTransfer,
		HealthIssue = @HealthIssue
	WHERE
		ID = @ID
RETURN 0
