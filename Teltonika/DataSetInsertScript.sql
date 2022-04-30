CREATE TABLE [dbo].[#temp](
	[X] [real] NULL,
	[Y] [real] NULL,
	[case_code] [nvarchar](max) NULL,
	[confirmation_date] [nvarchar](max) NOT NULL,
	[municipality_code] [nvarchar](max) NULL,
	[municipality_name] [nvarchar](max) NULL,
	[age_bracket] [nvarchar](max) NULL,
	[gender] [nvarchar](max) NULL,
	[object_id] [int] NOT NULL,
) 
--drop table #temp
BULK INSERT dbo.#temp
FROM 'C:\COVID19 cases.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)
SET IDENTITY_INSERT Covid19Cases ON
INSERT INTO Covid19Cases ([object_id],[gender], [age_bracket],[municipality_name], [municipality_code], [confirmation_date], [case_code], [X], [Y])
SELECT [object_id],[gender], [age_bracket],[municipality_name], [municipality_code], cast([confirmation_date] +':00' as datetimeoffset), [case_code], [X], [Y]  FROM dbo.#temp
SET IDENTITY_INSERT Covid19Cases OFF

DROP TABLE dbo.#temp