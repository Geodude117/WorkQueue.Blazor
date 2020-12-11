CREATE PROCEDURE [dbo].[DomainGroup_Insert]
	@GroupName NVARCHAR(100),
	@ClassMapping NVARCHAR(200),
	@ExternalReferenceId NVARCHAR(100),
	@IsActive  BIT,
	@AccessGroupPublic NVARCHAR(100),
	@AccessGroupBase NVARCHAR(100),
	@AccessGroupExtended NVARCHAR(100),
	@AccessGroupAdmin NVARCHAR(100)
AS
	INSERT INTO	[dbo].[DomainGroup]
		(
			[GroupName],
			[ClassMapping],
			[ExternalReferenceId],
			[IsActive],
			[AccessGroupPublic],
			[AccessGroupBase],
			[AccessGroupExtended],
			[AccessGroupAdmin]
		)	
	VALUES 
		(
			@GroupName,
			@ClassMapping,
			@ExternalReferenceId,
			@IsActive,
			@AccessGroupPublic,
			@AccessGroupBase,
			@AccessGroupExtended,
			@AccessGroupAdmin
			
		)
RETURN SCOPE_IdENTITY()
 