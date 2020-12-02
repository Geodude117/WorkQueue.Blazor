CREATE PROCEDURE [dbo].[DomainInformation_Insert]
	@Title NVARCHAR(100),
	@PropertyMapping NVARCHAR(100),
	@Order INT,
	@TypeId NVARCHAR(100),
	@GroupId  INT,
	@Arguments NVARCHAR(100),
	@HasValidation BIT
AS
	INSERT INTO	[dbo].[DomainInformation]
		(
			[Title],
			[PropertyMapping],
			[Order],
			[TypeId],
			[GroupId],
			[Arguments],
			[HasValidation]
		)	
	VALUES 
		(
			@Title,
			@PropertyMapping,
			@Order,
			@TypeId,
			@GroupId,
			@Arguments,
			@HasValidation
		)
RETURN SCOPE_IdENTITY()
