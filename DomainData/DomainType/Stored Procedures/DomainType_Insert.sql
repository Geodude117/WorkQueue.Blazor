CREATE PROCEDURE [dbo].[DomainType_Insert]
	@TypeName NVARCHAR(100),
	@ClassObject NVARCHAR(100)
AS
	INSERT INTO	[dbo].[DomainType]
		(
			[TypeName],
			[ClassObject]
		)	
	VALUES 
		(
			@TypeName,
			@ClassObject
		)
RETURN SCOPE_IdENTITY()
