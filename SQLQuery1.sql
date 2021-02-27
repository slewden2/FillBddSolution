
/*
SELECt  OBJECT_SCHEMA_NAME(t.object_id) as [schema], t.name as [table], c.name as [columns], c.column_id
, c.max_length, c.precision, c.scale
, c.collation_name, c.is_nullable
, c.is_rowguidcol, c.is_identity, c.is_computed, y.name
--,c.* , y.*
From sys.tables t 
INNER JOIN sys.columns c ON t.object_id = c.object_id
INNER JOIN sys.types y On c.system_type_id = y.system_type_id AND c.user_type_id = y.user_type_id
WHERE t.object_id =  OBJECT_ID('[dbo].[tableType]')

CREATE table [dbo].[tableType] (number Decimal(10,4) null)

--SELECt * From dbo.table1


---INSERT INTO dbo.table1 ([c2]) VALUES ('Texte1')

SELECt * From sys.types order by name

SELECt * From sys.types WHERE name in ('bigInt', 'int', 'smallint', 'tinyInt')  order by precision

DECLARE @dt DateTimeOffset
SET @dt = '2021-02-22 12:54:29.896000 +04:00'
SELECT @dt
*/


SELECT  OBJECT_SCHEMA_NAME(t.object_id) as [schema], t.name as [table], c.name as [column], c.column_id as [order]
, y.name as type_name, c.max_length, c.precision
, c.is_nullable, c.is_rowguidcol, c.is_identity, c.is_computed
From sys.tables t 
INNER JOIN sys.columns c ON t.object_id = c.object_id
INNER JOIN sys.types y On c.system_type_id = y.system_type_id AND c.user_type_id = y.user_type_id
WHERE t.object_id =  OBJECT_ID('[dbo].[demo]')


SELECt * From dbo.demo