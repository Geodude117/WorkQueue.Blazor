
CREATE PROCEDURE [callback].[GdprAnonymiseMe]
	@WescotRef int
AS

	UPDATE
		callback.CallbackDetail
	SET
		NameOfCaller = 'Wes Cot',
		Relationship = null,
		ContactNumber = '0110100A0D0W',
		TimeToAvoid = null,
		ReasonForCallback = 'Name None',
		ReasonForTransfer = 'None',
		HealthIssue = null
	WHERE
		WescotRef = @WescotRef

RETURN 0