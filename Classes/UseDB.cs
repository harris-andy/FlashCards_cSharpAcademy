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
                        score FLOAT,
                        StackId INT,
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

        public List<Stack> GetAllStackNames()
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            string getStackNames = @"SELECT Id, name FROM stacks;";
            List<Stack> stackData = connection.Query<Stack>(getStackNames).ToList();
            return stackData;
        }

        public int CreateStackID(string name)
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

        public void DeleteStack(int id)
        {
            using var connection = new SqlConnection(AppConfig.ConnectionString);
            var parameters = new { ID = id };
            string sql = @"DELETE FROM stacks WHERE stacks.Id = @ID;";
            connection.Execute(sql, parameters);
        }
    }
}