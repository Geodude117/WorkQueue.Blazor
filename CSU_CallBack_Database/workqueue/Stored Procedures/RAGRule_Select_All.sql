
create procedure workqueue.RAGRule_Select_All
as

select
	[r].[RAGRuleID],
	[r].[RAGStatus],
	[r].[LowValue], 
	[r].[MidValue],
	[r].[HighValue],
	[r].[LowToHigh]
from
	workqueue.RAGRule r