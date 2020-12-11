
CREATE PROCEDURE [dbo].[usp_RebuildIndexes]
	-- Add the parameters for the stored procedure here
	@FragmentationLevel		[INT]				=	 30,
	@MinPages			[INT]				=	500,
	@Rebuild	[INT]	=	0
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @SQLStatement		[varchar](max),@StatementID int,@MaxID INT
	
	-- Does audit table exist
	IF OBJECT_ID('WCSDBA..DatabaseFragementationList') IS NULL
	BEGIN  
		CREATE TABLE WCSDBA..[DatabaseFragementationList](
			[ID] [int] IDENTITY(1,1) NOT NULL,
			[cmd] [nvarchar](275) NULL,
			[DBName] [nvarchar](128) NULL,
			[Schemaname] [sysname] NOT NULL,
			[Tablename] [nvarchar](128) NULL,
			[Index_Description] [varchar](19) NULL,
			[Indexname] [sysname] NULL,
			[Fragmentation%] [float] NULL,
			[IndexSizeKB] [bigint] NULL,
			[page_count] [bigint] NULL,
			[DateAdded] [datetime] NULL,
			[DateReindexed] [datetime] NULL,	
		 CONSTRAINT [PK_DatabaseFragementationList] PRIMARY KEY CLUSTERED 
		(
			[ID] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]
		
		ALTER TABLE WCSDBA..[DatabaseFragementationList] ADD  DEFAULT (GETDATE()) FOR [DateAdded]
		
	END		

	SELECT @MaxID = ISNULL(MAX(ID),0) FROM WCSDBA..DatabaseFragementationList 	
	
	--Creating temporary tables.
	IF OBJECT_ID('Tempdb.dbo.#SQLStatementsStore') IS NOT NULL  
		DROP TABLE #SQLStatementsStore 
	CREATE TABLE #SQLStatementsStore ([SQLStatementID]	[int]  
									 ,[SQLstatement]	[varchar](1024))

	INSERT INTO WCSDBA..DatabaseFragementationList ([cmd],[DBName],[Schemaname],[Tablename],[Index_Description],[Indexname],[Fragmentation%],[IndexSizeKB],[page_count])
	SELECT    'ALTER INDEX ['+b.name+'] ON ['+s.name+N'].['+OBJECT_NAME(ps.OBJECT_ID)+N'] REBUILD WITH  (Online = ON, SORT_IN_TEMPDB = ON);' AS CMD,DB_NAME(ps.database_id) AS DBName,S.name AS Schemaname,OBJECT_NAME(ps.OBJECT_ID) AS Tablename, Index_Description = CASE WHEN ps.index_id = 1 THEN 'Clustered Index'  WHEN ps.index_id <> 1 THEN 'Non-Clustered Index'  END,   
	b.name AS Indexname,  
	ROUND(ps.avg_fragmentation_in_percent,0,1) AS 'Fragmentation%',  
	SUM(page_count*8) AS 'IndexSizeKB',  
	ps.page_count  
	FROM sys.dm_db_index_physical_stats (DB_ID(),NULL,NULL,NULL,NULL) AS ps  
	INNER JOIN sys.indexes AS b ON ps.object_id = b.object_id AND ps.index_id = b.index_id AND b.index_id <> 0 -- heap not required  
	INNER JOIN sys.tables AS t ON t.object_id = b.object_id
	INNER JOIN sys.objects AS O ON O.object_id=b.object_id AND O.type='U' AND O.is_ms_shipped=0 -- only user tables  
	INNER JOIN sys.schemas AS S ON S.schema_Id=O.schema_id  
	INNER JOIN sys.filegroups AS F ON b.data_space_id = f.data_space_id
	WHERE ps.database_id = DB_ID() 
	AND ps.avg_fragmentation_in_percent > @FragmentationLevel 
	AND ps.page_count  > @MinPages -- Indexes having more than 60% fragmentation 
		AND F.is_Read_Only = 'False'
	AND t.name NOT IN (SELECT   DISTINCT T.name
							FROM sys.indexes AS b -- heap not required  
							INNER JOIN sys.tables AS t ON t.object_id = b.object_id	INNER JOIN sys.objects AS O ON O.object_id=b.object_id AND O.type='U' AND O.is_ms_shipped=0 -- only user tables  
	INNER JOIN sys.schemas AS S ON S.schema_Id=O.schema_id AND b.type = 6)
	GROUP BY DB_NAME(ps.database_id),S.name,OBJECT_NAME(ps.OBJECT_ID),CASE WHEN ps.index_id = 1 THEN 'Clustered Index' WHEN ps.index_id <> 1 THEN 'Non-Clustered Index' END,b.name,ROUND(ps.avg_fragmentation_in_percent,0,1),ps.avg_fragmentation_in_percent,ps.page_count  
	ORDER BY ps.avg_fragmentation_in_percent DESC

	INSERT INTO #SQLStatementsStore (SQLStatementID,SQLstatement)
    SELECT ID,CMD  FROM WCSDBA..DatabaseFragementationList
	WHERE ID > @MaxID
		
	SELECT @StatementID = MIN(SQLStatementID) FROM #SQLStatementsStore

	WHILE @StatementID IS NOT NULL
		BEGIN
			SELECT 	@SQLStatement = SQLstatement
			FROM #SQLStatementsStore
			WHERE SQLStatementID = @StatementID
	
			IF (@Rebuild = 1) 
				BEGIN
					-- Record start date
					UPDATE WCSDBA..DatabaseFragementationList
					SET DateAdded = GETDATE()	
					WHERE ID = @StatementID
		
					-- Rebuild index
					EXEC (@SQLStatement)
				
					-- Record end date
					UPDATE WCSDBA..DatabaseFragementationList
					SET DateReindexed = GETDATE()	
					WHERE ID = @StatementID

				END

			DELETE FROM #SQLStatementsStore 
			WHERE SQLStatementID = @StatementID

			SELECT @StatementID = MIN(SQLStatementID) 
			FROM #SQLStatementsStore
		END

END;