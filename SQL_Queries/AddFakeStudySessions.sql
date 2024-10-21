-- Declare a variable to hold the date and other parameters
DECLARE @StackId INT;
DECLARE @Score INT;
DECLARE @Questions INT;

-- Insert three study sessions for each month from January to September 2024
DECLARE @Month INT;

-- Loop through the months 1 to 9 (January to September)
SET @Month = 1;

WHILE @Month <= 9
BEGIN
    -- Loop to insert three study sessions for the current month
    DECLARE @SessionCount INT = 1;
    
    WHILE @SessionCount <= 3
    BEGIN
        -- Randomly generate score and questions (ensuring score <= questions)
        SET @Questions = CAST(RAND() * 10 + 5 AS INT);  -- Generate random questions (5 to 14)
        SET @Score = CAST(RAND() * @Questions AS INT); -- Generate random score (0 to Questions)

        -- Insert a new study session
        INSERT INTO study_sessions (date, score, questions, stackId)
        VALUES 
        (DATEADD(MONTH, @Month - 1, '2024-01-01') + CAST(RAND() * 30 AS INT), @Score, @Questions, CAST(RAND() * 3 + 1 AS INT));

        SET @SessionCount = @SessionCount + 1;
    END

    SET @Month = @Month + 1;
END
