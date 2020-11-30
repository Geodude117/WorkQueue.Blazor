CREATE PROCEDURE [dbo].[DomainInformation_Insert]
	@Title NVARCHAR(100),
	@ObjectMapping NVARCHAR(100),
	@Order INT,
	@TypeId NVARCHAR(100),
	@GroupId  INT,
	@Arguments NVARCHAR(100),
	@HasValidation BIT
AS
	INSERT INTO	[dbo].[DomainInformation]
		(
			[Title],
			[ObjectMapping],
			[Order],
			[TypeId],
			[GroupId],
			[Arguments],
			[HasValidation]
		)	
	VALUES 
		(
			@Title,
			@ObjectMapping,
			@Order,
			@TypeId,
			@GroupId,
			@Arguments,
			@HasValidation
		)
RETURN SCOPE_IdENTITY()
