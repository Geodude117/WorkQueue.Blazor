
CREATE VIEW [workqueue].[Test]
AS
SELECT        qi.QueueItemID, qi.WescotRef, qi.CustomerName, qi.CompletedDate, qi.CompletedBy, qi.DueDate, qi.CreatedDate, qi.CreatedBy, qi.Summary, qi.ParentQueueItemID, qi.QueueID, r.RAGRuleID, r.RAGStatus, r.LowValue, 
                         r.MidValue, r.HighValue, r.LowToHigh, q.QueueGroupID, qi.LockTime
,
						 case when qi.LockTime is not null then 1 
						 else 0 end as IsLocked
FROM            workqueue.QueueItem AS qi INNER JOIN
                         workqueue.Queue AS q ON q.QueueID = qi.QueueID INNER JOIN
                         workqueue.RAGRule AS r ON r.RAGRuleID = q.RAGRuleID