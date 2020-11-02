USE master
GO

EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'HangFire'
GO

USE master
GO

DROP DATABASE IF EXISTS HangFire
GO