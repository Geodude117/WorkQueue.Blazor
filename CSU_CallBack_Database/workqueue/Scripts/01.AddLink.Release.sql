USE <WarehouseIC,varchar(1000),Warehouse>
GO

INSERT INTO [dbo].[WCS_TBL_Agent_BrowserLinks]
           ([ApplicationName]
           ,[ApplicationStage]
           ,[URL])
     VALUES
           ('CALLBACK',
           'Home',
		   'http://' + <URL, varchar(1000),'Workq'> +'/workqueue/create?WescotRef={1}&CustomerName={0}&TelphoneNumber={2}&MobileNumber={3}&EmployerTel={4}')
GO

