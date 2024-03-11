--CREATE DATABASE
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name='EMAIL_DB')
CREATE DATABASE [EMAIL_DB]
ON PRIMARY (
    NAME = N'<Data_File_Name, sysname, EMAIL_DB>',
    FILENAME = N'<Data_File_Path, nvarchar(max), C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\EMAIL_DB.mdf>',
	SIZE = 512MB
)
LOG ON (
    NAME = N'<Log_File_Name, sysname, EMAIL_DB_LOG>',
    FILENAME = N'<Log_File_Path, nvarchar(max), C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\EMAIL_DB.ldf>'
)
GO
