/******************************************************************************
<copyright file="CartridgeProcessingQueue.sql" company="Sequoia Voting Systems">
Copyright (c) 2010 Sequoia Voting Stsyems. All Rights Reserved.
</copyright>

<summary>
Contains statements to generate tables and procedures necessary for the
Authentication service to work.
</summary>
******************************************************************************/

USE [CartridgeProcessingQueue]
GO
/****** Object:  Table [dbo].[AuthDbUser]    Script Date: 04/12/2010 11:57:51 ******/
CREATE TABLE [dbo].[AuthDbUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DbName] [varchar](50) NOT NULL,
	[DbUser] [nvarchar](128) NOT NULL,
	[Created] [datetime] NOT NULL DEFAULT (getdate()),
	[Modified] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_AuthDbUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)

GO

/****** Object:  Table [dbo].[LogEvent]    Script Date: 04/15/2010 10:24:13 ******/
CREATE TABLE [dbo].[LogEvent](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](max) NULL,
	[EventSourceId] [int] NULL,
	[EventSeverityId] [int] NULL,
	[DateCreated] [datetime] NULL DEFAULT (getdate()),
 CONSTRAINT [PK_LogEvent] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)

GO

/****** Object:  Table [dbo].[SQLErrorLog]    Script Date: 04/15/2010 10:48:44 ******/
CREATE TABLE [dbo].[SQLErrorLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[Component] [varchar](1000) NULL,
	[ErrorDtm] [datetime] NULL,
	[Number] [int] NULL,
	[Severity] [int] NULL,
	[State] [int] NULL,
	[Line] [int] NULL,
	[ErrMsg] [varchar](max) NULL,
 CONSTRAINT [PK_SQLErrorLog] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)

GO

/****** Object:  Procedure [dbo].[upCatchException]    Script Date: 04/12/2010 11:57:51 ******/
CREATE PROCEDURE [dbo].[upCatchException]
AS
SET NOCOUNT ON		-- Limits returns of rows affected messages

BEGIN
-- There is error information that can be used to build
-- an error message
IF ERROR_NUMBER() IS NOT NULL
-- Log the error
BEGIN TRY
INSERT INTO
SQLErrorLog
VALUES
(
ISNULL(ERROR_PROCEDURE(), '-')
,	GETDATE() -- time stamp of entry
,	ERROR_NUMBER()
,	ERROR_SEVERITY()
,	ERROR_STATE()
,   ERROR_LINE()
,   ERROR_MESSAGE()
)
END TRY
BEGIN CATCH
-- Do nothing if there is an error while writing to the log
END CATCH
END

/*** Object:  Procedure [dbo].[upAuthDbUserGet]    Script Date: 04/12/2010 11:57:51 ***/
CREATE PROCEDURE [dbo].[upAuthDbUserGet]

AS

SET NOCOUNT ON  -- Suppress rows affected messages
BEGIN TRY
SELECT
Id
,   DbName
,   DbUser
,   Created
,   Modified
FROM
AuthDbUser
END TRY
BEGIN CATCH
-- Execute upCatchException to log the error
EXEC upCatchException
RETURN 1
END CATCH


/*** Object:  Procedure [dbo].[upElectionUserAlter]  Script Date: 04/12/2010 11:57:51 ***/
CREATE PROCEDURE [dbo].[upElectionUserAlter]
(
@newLogin       nvarchar(128)
,   @password       nvarchar(256)
,   @dbName         nvarchar(500)
)
AS

SET NOCOUNT ON  -- Suppress rows affected messages

BEGIN TRY
BEGIN TRANSACTION
-- declare variable for sql statement
DECLARE
@sqlCmd         nvarchar(1000)
,   @role           nvarchar(20)
,   @oldLogin       nvarchar(50)

SELECT
@sqlCmd         = ''
,   @role           = 'sysadmin'
,   @oldLogin       = ''

SELECT
@oldLogin       = ISNULL(DbUser, '')
FROM
AuthDbUser
WHERE
DbName          = @dbName

IF @oldLogin        = ''
-- insert login entry into AuthDbUser table
INSERT INTO
AuthDbUser
SELECT
@dbName
,   @newLogin
,   GETDATE()
,   GETDATE()
ELSE
-- update entry
UPDATE
AuthDbUser
SET
DbUser      = @newLogin
,   Modified    = GETDATE()
WHERE
DbName      = @dbName

-- check if login already exists, if not create new login, if login
-- already exists, alter existing login
IF EXISTS
(SELECT
1
FROM
sys.sql_Logins
WHERE
name            = @oldLogin
)
BEGIN
SELECT
@sqlCmd     = 'ALTER LOGIN '
+ quotename(@oldLogin)
+ ' WITH NAME = '
+ quotename(@newLogin)
+ ', PASSWORD = '
+ quotename(@password, '''')
END
ELSE
BEGIN
SELECT
@sqlCmd     = 'CREATE LOGIN '
+ quotename(@newLogin)
+ ' WITH PASSWORD = '
+ quotename(@password, '''')
END

EXECUTE (@sqlCmd)
COMMIT TRANSACTION

EXEC sp_addsrvrolemember @loginame = @newLogin, @rolename = @role

-- select the return value, otherwise ExecuteScalar is returning NULL
SELECT 0

END TRY
BEGIN CATCH
-- Execute upCatchException to log the error
EXEC upCatchException
RETURN 1
END CATCH

GO

/*** Object:  Procedure [dbo].[upInsertLogMessage]    Script Date: 04/12/2010 11:57:51 ***/
CREATE PROCEDURE [dbo].[upInsertLogMessage]
(
@message            VARCHAR(MAX)
,   @eventSourceId      INT
,   @severityLevelId    INT
)
AS

SET NOCOUNT ON  -- Suppress rows affected messages

BEGIN TRY
-- insert messaeg into LogEvent
INSERT INTO
LogEvent
(
Message
, EventSourceId
, EventSeverityId
)
VALUES
(
@message
,   @eventSourceId
,   @severityLevelId
)

-- Return the Id of the inserted record
SELECT SCOPE_IDENTITY()

END TRY

BEGIN CATCH
-- Execute upCatchException to log the error
EXEC upCatchException

RETURN 1
END CATCH
GO