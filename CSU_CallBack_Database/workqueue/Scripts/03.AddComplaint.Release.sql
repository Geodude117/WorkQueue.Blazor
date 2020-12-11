USE <OldWarehouseDatabase,varchar(1000),Warehouse>
GO
/****** Object:  StoredProcedure [dbo].[WCS_CLOSURE_AddComplaint]    Script Date: 16/01/2019 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[WCS_CLOSURE_AddComplaint] @userid VARCHAR(255), @wescotreference VARCHAR(50), @clientid VARCHAR(50) = NULL, 
       @callid INT = NULL, @noteid INT, @noteversionid INT, @optionid INT, @optionversionid INT, @agentnotes VARCHAR(4000) = NULL,
       @agentresult VARCHAR(1000)
AS
/*
Author Graham Johnson
Date 04/10/2017
Creates a new complaint through NotesBuilder
*/
BEGIN
       DECLARE @closure_id INT;
       DECLARE @closure2 INT;
       DECLARE @client VARCHAR(50);

       SELECT @client = B.ClientOwningGroup
       FROM DimClientBranch B
       JOIN WCS_TBL_Enquiry_Lookup E ON B.ClientCode = E.ClientCode AND B.BranchCode = E.BranchCode
       WHERE E.WescotReference = @wescotreference;                     

       EXEC   @closure_id = [dbo].[WCS_CLOSURE_AddClosure]
                     @userid = @userid,
                     @clientid = @client,
                     @wescotreference = @wescotreference;

       SELECT @closure2 = MAX(closureid) 
       FROM dbo.WCS_TBL_CLOSURE_CallClosureID
       WHERE UserID = @userid
       AND WescotReference = @wescotreference;
              
       SET @closure_id = SCOPE_IDENTITY();
       
       EXEC   [dbo].[WCS_CLOSURE_AddResult]
                     @closureid = @closure2,
                     @noteid = @noteid,
                     @noteversionid = @noteversionid,
                     @optionid = @optionid, 
                     @optionversionid = @optionversionid,
                     @agenttext = @agentresult;

       EXEC   [dbo].[WCS_CLOSURE_AddAgentNotes] 
                     @closureid = @closure2, -- int
                     @agentnotes = @agentnotes -- varchar(1000)
       
END