CREATE PROCEDURE [dbo].[DomainGroup_Insert]
	@GroupName NVARCHAR(100),
	@ExternalReferenceId NVARCHAR(100),
	@IsActive  BIT
AS
	INSERT INTO	[dbo].[DomainGroup]
		(
			[GroupName],
			[ExternalReferenceId],
			[IsActive]
		)	
	VALUES 
		(
			@GroupName,
			@ExternalReferenceId,
			@IsActive
			
		)
RETURN SCOPE_IdENTITY()
 