-- SELECT * FROM flashcards;

-- DELETE FROM flashcards;
-- DELETE FROM stacks;
-- DBCC CHECKIDENT ('stacks', RESEED, 0)
-- DBCC CHECKIDENT ('flashcards', RESEED, 0);
-- DBCC CHECKIDENT ('study_sessions', RESEED, 0);

-- EXEC sp_help 'flashcards';



-- ALTER TABLE flashcards 
ALTER TABLE study_sessions
ALTER COLUMN score FLOAT;

-- ADD Id INT PRIMARY KEY IDENTITY(1,1);



-- USE master;
-- GO

-- ALTER DATABASE FlashCards SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
-- GO
-- DROP DATABASE FlashCards;
-- GO
