-- SELECT * FROM flashcards;
-- DELETE FROM flashcards;
-- DELETE FROM stacks;
-- DBCC CHECKIDENT ('stacks', RESEED, 1)
-- DBCC CHECKIDENT ('flashcards', RESEED, 1);

-- EXEC sp_help 'flashcards';

-- DBCC CHECKIDENT ('flashcards', RESEED, 0)
-- DBCC CHECKIDENT ('stacks', RESEED, 0)
-- DBCC CHECKIDENT ('stack_flashcards', RESEED, 0)
-- DBCC CHECKIDENT ('study_sessions', RESEED, 0);

-- ALTER TABLE stack_flashcards 
-- ADD Id INT PRIMARY KEY IDENTITY(1,1);



-- USE master;
-- GO

-- ALTER DATABASE FlashCards SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
-- GO
-- DROP DATABASE FlashCards;
-- GO



-- ALTER TABLE stack_flashcards
-- DROP CONSTRAINT PK__stack_fl__DC2109508123665E;

-- ALTER TABLE stack_flashcards
-- ADD Id INT IDENTITY(1,1) PRIMARY KEY;
