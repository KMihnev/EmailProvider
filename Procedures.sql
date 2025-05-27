USE EMAIL_DB
GO

--Добавяне на описание на колона
IF EXISTS (SELECT 1 FROM sys.procedures WHERE name = N'SP_ADD_COLUMN_DESCRIPTION')
BEGIN
    EXEC sp_dropextendedproperty N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'SP_ADD_COLUMN_DESCRIPTION'
    DROP PROCEDURE [SP_ADD_COLUMN_DESCRIPTION]
END
GO

CREATE PROCEDURE SP_ADD_COLUMN_DESCRIPTION
    @DESCRIPTION NVARCHAR(MAX),
    @TABLE_NAME NVARCHAR(128),
    @COLUMN_NAME NVARCHAR(128)
AS
BEGIN
    DECLARE @SCHEMA_NAME NVARCHAR(128) = 'dbo'
    DECLARE @Sql NVARCHAR(MAX)

    SET @Sql = N'EXEC sys.sp_addextendedproperty '
           + N'@name = N''MS_Description'', '
           + N'@value = N''' + @DESCRIPTION + ''', '
           + N'@level0type = N''SCHEMA'', '
           + N'@level0name = N''' + @SCHEMA_NAME + ''', '
           + N'@level1type = N''TABLE'', '
           + N'@level1name = N''' + @TABLE_NAME + ''', '
           + N'@level2type = N''COLUMN'', '
           + N'@level2name = N''' + @COLUMN_NAME + ''';';
    EXEC sp_executesql @Sql
END
GO

EXEC sp_addextendedproperty N'MS_Description', N'Stored procedure to add a description to a column', N'SCHEMA', N'dbo', N'PROCEDURE', N'SP_ADD_COLUMN_DESCRIPTION'
GO


-- Сваляне на таблица
IF EXISTS (SELECT 1 FROM sys.procedures WHERE name = N'SP_DROP_TABLE')
BEGIN
    EXEC sp_dropextendedproperty N'MS_Description',N'SCHEMA',N'dbo',N'PROCEDURE',N'SP_DROP_TABLE'
    DROP PROCEDURE [SP_DROP_TABLE]
END
GO

CREATE PROCEDURE [SP_DROP_TABLE]
    @TABLE_NAME NVARCHAR(128)
AS
BEGIN
    IF EXISTS 
	(
        SELECT * FROM sys.objects
       WHERE object_id=OBJECT_ID(@TABLE_NAME) AND type = 'U'
    )
    BEGIN
        DECLARE @SQL NVARCHAR(MAX)
        DECLARE @COLUMN_NAME NVARCHAR(128)

        DECLARE ColumnCursor CURSOR FOR
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = @TABLE_NAME AND TABLE_SCHEMA = 'dbo'

        OPEN ColumnCursor
        FETCH NEXT FROM ColumnCursor INTO @COLUMN_NAME

        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @SQL = 'EXEC sp_dropextendedproperty N''MS_Description'', N''SCHEMA'', N''dbo'', N''TABLE'', N''' + @TABLE_NAME + ''', N''COLUMN'', N''' + @COLUMN_NAME + ''''
            EXEC sp_executesql @SQL
            FETCH NEXT FROM ColumnCursor INTO @COLUMN_NAME
        END

        CLOSE ColumnCursor
        DEALLOCATE ColumnCursor

        SET @SQL = N'DROP TABLE dbo.[' + @TABLE_NAME + ']'
        EXEC sp_executesql @SQL
    END
END
GO

EXEC sp_addextendedproperty N'MS_Description', N'Stored procedure to drop a table with all column descriptions', N'SCHEMA', N'dbo', N'PROCEDURE', N'SP_DROP_TABLE'
GO