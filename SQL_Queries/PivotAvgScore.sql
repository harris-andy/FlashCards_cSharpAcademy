SELECT * FROM (
    SELECT
        -- study_sessions.Id AS StudyID, 
        ISNULL(CAST(SUM(study_sessions.score) AS FLOAT) / CAST(SUM(study_sessions.questions) AS FLOAT), 0) AS Grade,
        -- study_sessions.score AS Score,
        -- study_sessions.questions as Questions,
        stacks.name AS StackName,
        CASE MONTH(date)
                WHEN 1 THEN 'January'
                WHEN 2 THEN 'February'
                WHEN 3 THEN 'March'
                WHEN 4 THEN 'April'
                WHEN 5 THEN 'May'
                WHEN 6 THEN 'June'
                WHEN 7 THEN 'July'
                WHEN 8 THEN 'August'
                WHEN 9 THEN 'September'
                WHEN 10 THEN 'October'
                WHEN 11 THEN 'November'
                WHEN 12 THEN 'December'
        END AS MonthDate
    FROM study_sessions
    JOIN stacks ON stacks.Id  = study_sessions.StackId
    GROUP BY stacks.name, MONTH(date)
) temp
PIVOT (
    AVG(Grade) FOR MonthDate IN ([January], [February], [March], [April], [May], [June], [July], [August], [September], [October], [November], [December])
) AS session_count_pivot;

-- SUM(
--   quantity * list_price * (1 - discount)
--     ) net_value

-- COUNT(StudyId)
