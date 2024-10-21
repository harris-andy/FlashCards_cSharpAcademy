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
        public void InitializeDatabase()
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
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('stacks') AND type in ('U'))
                BEGIN
                    CREATE TABLE stacks(
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        name NVARCHAR(255) NOT NULL
                    );
                END;

                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('flashcards') AND type in ('U'))
                BEGIN
                    CREATE TABLE flashcards (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        front NVARCHAR(255),
                        back NVARCHAR(255),
                        StackId INT,
                        FOREIGN KEY (StackId) REFERENCES stacks(Id) ON DELETE CASCADE
                    );
                END;
                
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('study_sessions') AND type in ('U'))
                BEGIN
                    CREATE TABLE study_sessions (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        date DATETIME,
                        score INT,
                        questions INT,
                        stackId INT,
                        FOREIGN KEY (StackId) REFERENCES stacks(Id) ON DELETE CASCADE
                    );
                END;";

            connection.Execute(createTables);
        }

        public void AddFlashCard(FlashCard flashCard, int stackID)
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            var parameters = new { Front = flashCard.Front, Back = flashCard.Back, StackID = stackID };
            // string sql = "INSERT INTO flashcards (front, back, stackID) OUTPUT INSERTED.Id VALUES (@Front, @Back);";
            string sql = "INSERT INTO flashcards (front, back, stackID) VALUES (@Front, @Back, @StackID);";
            connection.Execute(sql, parameters);
            // int flashCardID = connection.QuerySingle<int>(sql, parameters);
            // Console.WriteLine($"Flash card ID: {flashCardID}");
            // Thread.Sleep(5000);
            // return flashCardID;
        }

        public int AddStack(string name)
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            var parameters = new { Name = name };
            string stackQuery = @"
                IF NOT EXISTS (SELECT Id FROM stacks WHERE name = @Name) 
                    BEGIN 
                        INSERT INTO stacks (name) VALUES (@Name); 
                    END; 
                SELECT Id FROM stacks WHERE name = @Name;";
            int stackId = connection.QuerySingle<int>(stackQuery, parameters);
            // connection.Execute(stackQuery, parameters);

            // var stackId = connection.QuerySingle<int>(
            // "IF NOT EXISTS (SELECT Id FROM stacks WHERE name = @name) BEGIN INSERT INTO stacks (name) VALUES (@name); END; SELECT Id FROM stacks WHERE name = @name;",
            // new { name = stackName });

            return stackId;
        }

        public void AddFakeData(string filePath)
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            var sql = File.ReadAllText(filePath);
            connection.Execute(sql);

        }

        public void AddStudySession(StudySessionRecord record)
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            var parameters = new { Date = record.Date, Score = record.Score, Questions = record.Questions, StackID = record.StackID };
            string sql = "INSERT INTO study_sessions (date, score, questions, stackID) VALUES (@Date, @Score, @Questions, @StackID)";
            connection.Execute(sql, parameters);
        }

        public List<Stack> GetAllStackNames()
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            string getStackNames = @"SELECT Id, name FROM stacks;";
            return connection.Query<Stack>(getStackNames).ToList();
            // return stackData;
        }

        public List<StackDTO> GetStacksAndSessionCount()
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            string sql = @"
            SELECT 
                stacks.Id,
                stacks.name,
                COUNT(study_sessions.Id) AS session_count
            FROM 
                stacks
            LEFT JOIN 
                study_sessions ON stacks.Id = study_sessions.stackId
            GROUP BY 
                stacks.Id, stacks.name;";
            return connection.Query<StackDTO>(sql).ToList();
        }

        public List<FlashCardDTO> GetFlashCardDTO(int stackID)
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            var parameters = new { ID = stackID };
            string sql = @"SELECT front, back FROM flashcards WHERE stackID = @ID";
            return connection.Query<FlashCardDTO>(sql, parameters).ToList();
        }

        public string GetStackName(int stackID)
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            var parameters = new { ID = stackID };
            string sql = "SELECT name FROM stacks WHERE Id = @ID";
            return connection.QuerySingle<string>(sql, parameters);
        }

        public List<StudySessionDTO> GetStudySessionRecords(int stackID)
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            var parameters = new { ID = stackID };
            string sql = @"SELECT date, score, questions FROM study_sessions WHERE StackId = @ID";
            return connection.Query<StudySessionDTO>(sql, parameters).ToList();
        }

        public void DeleteStack(int stackID)
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            var parameters = new { ID = stackID };
            string sql = @"DELETE FROM stacks WHERE stacks.Id = @ID;";
            connection.Execute(sql, parameters);
        }

        public void GetStudySessionCounts()
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            var sql = File.ReadAllText("./SQL_Queries/PivotCounts.sql");
            List<StudySessionReport> records = connection.QuerySingle<StudySessionReport>(sql).ToList();
        }
    }
}