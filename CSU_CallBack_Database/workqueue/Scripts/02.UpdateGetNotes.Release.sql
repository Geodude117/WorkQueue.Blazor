USE <WarehouseIC,varchar(1000),Warehouse>
GO

/****** Object:  StoredProcedure [dbo].[WCS_CLOSURE_GetNotes]    Script Date: 14/12/2018 10:10:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[WCS_CLOSURE_GetNotes] @sectionid int, @wescotreference varchar(20)
as
/*
Author Martin Ingham
Date 18/01/2013
Description Gets all notes for a specfied section
			checks if a note should be visible for the account
*/
begin

       WITH Temp
       as
       (
              SELECT N.NoteID, N.NoteVersionID, Note, Visible, ControlTypeID, N.NoteFixedText, N.Sequence, isnull(F.FieldSize, 0) FieldSize
              FROM WCS_TBL_CLOSURE_Note N
              LEFT JOIN WCS_TBL_CLOSURE_NoteFormat F ON N.NoteID = F.NoteID
              WHERE SectionID = @sectionid
              AND N.StartDate <=
			  dateadd(d, 0, datediff(d, 0, getdate())) AND (N.EndDate IS NULL OR N.EndDate > dateadd(d, 0, datediff(d, 0, getdate())))
       )
       SELECT T.NoteID, T.NoteVersionID, T.Note, T.Visible, T.ControlTypeID, T.NoteFixedText, T.Sequence, FieldSize
       FROM Temp T
       WHERE NOT EXISTS (SELECT A.NoteID 
                                  FROM WCS_TBL_CLOSURE_Account A
                                  WHERE A.NoteID = T.NoteID)
       UNION 
       SELECT T.NoteID, T.NoteVersionID, T.Note, T.Visible, T.ControlTypeID, T.NoteFixedText, T.Sequence, FieldSize
       FROM Temp T
       JOIN (SELECT B.ClientOwningGroup, A.NoteID
              FROM WCS_TBL_Enquiry_Lookup E
              JOIN DimClientBranch B ON E.ClientCode = B.ClientCode AND E.BranchCode = B.BranchCode 
              JOIN WCS_TBL_CLOSURE_Account A ON B.ClientOwningGroup = A.ClientAccount
              WHERE E.WescotReference = @wescotreference) A ON T.NoteID = A.NoteID
       ORDER BY Sequence
end
GO