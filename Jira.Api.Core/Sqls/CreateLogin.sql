USE master
GO

IF DB_NAME() <> N'master'
  SET NOEXEC ON
GO


--
-- Set transaction isolation level
--
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

--
-- Start Transaction
--
BEGIN TRANSACTION
GO

--
-- Create login [jira]
--
CREATE LOGIN jira
WITH
PASSWORD = 'New1Password!'
GO
IF @@ERROR <> 0
  OR @@TRANCOUNT = 0
BEGIN
  IF @@TRANCOUNT > 0
    ROLLBACK
  SET NOEXEC ON
END
GO

--
-- Commit Transaction
--
IF @@TRANCOUNT > 0
  COMMIT TRANSACTION
GO

--
-- Add members to the role [sysadmin]
--
EXEC sp_addsrvrolemember N'jira'
                        ,N'sysadmin'
GO
IF @@ERROR <> 0
  SET NOEXEC ON
GO

--
-- Add members to the role [dbcreator]
--
EXEC sp_addsrvrolemember N'jira'
                        ,N'dbcreator'
GO
IF @@ERROR <> 0
  SET NOEXEC ON
GO

--
-- Set NOEXEC to off
--
SET NOEXEC OFF
GO