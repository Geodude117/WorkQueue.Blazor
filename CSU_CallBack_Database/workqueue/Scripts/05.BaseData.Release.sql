-- Base Data for Queue Tables

-- Queue Groups
INSERT INTO workqueue.QueueGroup (QueueGroupID, [Name], IsActive, AccessGroupAdmin, AccessGroupBase, AccessGroupExtended, AccessGroupPublic, DefaultQueueID)
VALUES (1, 'CSU Callback', 1, 'adminGroup', 'baseGroup', 'extendedGroup', NULL, 1),
		(2, 'CSU Callback Letter', 1, 'adminGroup', 'baseGroup', 'extendedGroup', NULL, 3),
		(3, 'CSU Callback Letter Review', 1, 'adminGroup', 'baseGroup', 'extendedGroup', NULL, 4)
GO

-- RAG Rules
INSERT INTO workqueue.RAGRule (RAGRuleID, RAGStatus, LowToHigh, LowValue, MidValue, HighValue)
VALUES (1, 'CSU Day 1', 1, 0, 3, 7),
	   (2, 'CSU Day 2', 1, 0, 3, 7)
GO

-- Queues
-- CSU has two queues, one for intitial and a second for call back retries
INSERT INTO workqueue.[Queue] (QueueID, QueueGroupID, IsActive, [Name], RAGRuleID)
VALUES (1, 1, 1, 'CSU Q #1', 1),
	   (2, 1, 1, 'CSU Q #2', 1),
	   (3, 2, 1, 'CSU Callback Letters', 1),
	   (4, 3, 1, 'CSU Callback Letters Review & Issues', 1)
GO

-- Action Types
-- CSU requires the following action types Add Diary Note, Add To Queue, Pass To IDV
INSERT INTO workqueue.ActionType (ActionID, ActionDescription)
VALUES (1, 'Diary Note'),
	   (2, 'Create Callback'),
	   (3, 'Create Letter')
GO

-- Queue Result
-- For each queue, a possible list of responses are available
-- Queue CSU Q #1 (1)
INSERT INTO workqueue.QueueResult (QueueResultID, QueueResult, QueueID)
VALUES (1, 'Success', 1),
	   (2, 'Voice mail left', 1),
	   (3, 'No Answer (No Response)', 1),
	   (4, 'Bad Number', 1),
	   (5, 'Auth 3rd Party Message Left', 1),
	   (6, 'UnAuthorised 3rd Party', 1),
	   (7,'Not Required', 1)
GO
-- Queue CSU Q #2 (2)
INSERT INTO workqueue.QueueResult (QueueResultID, QueueResult, QueueID)
VALUES (8, 'Voice mail left', 2),
	   (9, 'No Answer (No Response)', 2),
	   (10,'Bad Number', 2),
	   (11,'Auth 3rd Party Message Left', 2),
	   (12,'UnAuthorised 3rd Party', 2),
	   (13, 'Success', 2),
	   (14,'Not Required', 2)
GO
-- Queue CSU Callback Letters (3)
INSERT INTO workqueue.QueueResult (QueueResultID, QueueResult, QueueID)
VALUES
	   (15,'Created', 3),
	   (16,'Not Required', 3)
GO
-- Queue CSU Callback Letters Review & Issues (4)
INSERT INTO workqueue.QueueResult (QueueResultID, QueueResult, QueueID)
VALUES
	   (17,'Sent', 4),
	   (18,'Not Required', 4)
GO

-- Queue ActionResult Mapping

-- Queue CSU Q #1 (1)
INSERT INTO workqueue.QueueAction (QueueActionID, QueueResultID, ActionID, ActionCode  ,DefaultDays)
VALUES  (1,3,1,'ONR',1),
		(2,4,1,'OIN',1),
		(3,3,2,'2',3),
		(4,2,2,'2',3),
		(5,6,2,'2',3),
		(6,2,1,'OMA',1),
		(7,6,1,'ODP',1),
		(8,5,1,'OML',1)
GO

-- Queue CSU Q #2 (2)
INSERT INTO workqueue.QueueAction (QueueActionID, QueueResultID, ActionID, ActionCode  ,DefaultDays)
VALUES
		(9,8,1,'OMA',1),
		(10,9,1,'ONR',1),
		(11,10,1,'OIN',1), 
		(12,11,1,'OML',1),
		(13,12,1,'ODP',1),
		(14,8,3,'3',1),
		(15,9,3,'3',1), 
		(16,12,3,'3',1)
GO

-- Queue CSU Callback Letters (3)
INSERT INTO workqueue.QueueAction (QueueActionID, QueueResultID, ActionID, ActionCode  ,DefaultDays)
VALUES
		(17,15,3,'4',1)
GO