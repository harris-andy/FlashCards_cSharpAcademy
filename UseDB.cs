using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Flashcards.harris_andy
{
    public class UseDB
    {
        public static void InitializeDatabase()
        {
            string connectionStringMaster = "Server=localhost,1433;Database=master;User Id=SA;Password=SuperStrongSexyPassword123;TrustServerCertificate=True;";

            using var connectionMaster = new SqlConnection(connectionStringMaster);

            var checkDbQuery = @"
                IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'FlashCards')
                    BEGIN
                    CREATE DATABASE FlashCards;
                    END";
            connectionMaster.Execute(checkDbQuery);

            using var connection = new SqlConnection(AppConfig.ConnectionString);

            var createTables = @"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('flashcards') AND type in ('U'))
                BEGIN
                    CREATE TABLE flashcards (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        front TEXT,
                        back TEXT
                    );
                END;

                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('stacks') AND type in ('U'))
                BEGIN
                    CREATE TABLE stacks(
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        name TEXT NOT NULL
                    );
                END;

                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('stack_flashcards') AND type in ('U'))
                BEGIN
                    CREATE TABLE stack_flashcards (
                        StackId INT,
                        FlashcardId INT,
                        PRIMARY KEY (StackId, FlashcardId),
                        FOREIGN KEY (StackId) REFERENCES stacks(Id) ON DELETE CASCADE,
                        FOREIGN KEY (FlashcardId) REFERENCES flashcards(Id)
                    );
                END;
                
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('study_sessions') AND type in ('U'))
                BEGIN
                    CREATE TABLE study_sessions (
                        Id INT PRIMARY KEY INDENTITY(1,1),
                        date DATETIME,
                        score INT,
                        FOREIGN KEY (StackId) REFERENCES stacks(Id)
                    );
                END;";

            connection.Execute(createTables);
        }

        public static void AddFlashCard()
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            // string front = "What is the capital of France?";
            // string back = "Paris";
            // var addFlashCard = @"INSERT INTO flashcards (front, back) VALUES (@Front, @Back);";
        }
    }
}