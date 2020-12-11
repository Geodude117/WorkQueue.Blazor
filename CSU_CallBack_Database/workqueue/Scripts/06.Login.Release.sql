USE [master]

	CREATE LOGIN [wescot\SVC_L_CSU] FROM WINDOWS WITH DEFAULT_DATABASE=[WorkQueues], DEFAULT_LANGUAGE=[us_english]
GO

USE [WorkQueues]
	CREATE USER [wescot\SVC_L_CSU] FOR LOGIN  [wescot\SVC_D_CSU]
		WITH DEFAULT_SCHEMA = [dbo];
		
	GRANT Execute ON SCHEMA::[callback] to [wescot\SVC_D_CSU];
	GRANT Execute ON SCHEMA::[workqueue] to [wescot\SVC_D_CSU];
	
GO

