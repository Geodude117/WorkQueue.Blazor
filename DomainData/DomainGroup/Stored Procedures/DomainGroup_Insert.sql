CREATE PROCEDURE [dbo].[DomainGroup_Insert]
	@GroupName NVARCHAR(100),
	@ClassMapping NVARCHAR(200),
	@ExternalReferenceId NVARCHAR(100),
	@IsActive  BIT
AS
	INSERT INTO	[dbo].[DomainGroup]
		(
			[GroupName],
			[ClassMapping],
			[ExternalReferenceId],
			[IsActive]
		)	
	VALUES 
		(
			@GroupName,
			@ClassMapping,
			@ExternalReferenceId,
			@IsActive
			
		)
RETURN SCOPE_IdENTITY()
 