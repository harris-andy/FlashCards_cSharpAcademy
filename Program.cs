using Flashcards.harris_andy;
using Microsoft.Data.SqlClient;
using Dapper;

internal class Program
{
    private static void Main(string[] args)
    {
        string connectionStringMaster = "Server=localhost,1433;Database=master;User Id=SA;Password=SuperStrongSexyPassword123;TrustServerCertificate=True;";
        string connectionStringFlashCards = AppConfig.ConnectionString;

        using var connectionMaster = new SqlConnection(connectionStringMaster);

        var checkDbQuery = @"
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'FlashCards')
                BEGIN
                CREATE DATABASE FlashCards;
                END";
        connectionMaster.Execute(checkDbQuery);


        using var connection = new SqlConnection(AppConfig.ConnectionString);

        var createTables = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'flashcards') AND type in (N'U'))
            BEGIN
                CREATE TABLE flashcards (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    front TEXT,
                    back TEXT
                );
            END;

            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'stacks') AND type in (N'U'))
            BEGIN
                CREATE TABLE stacks(
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    name TEXT NOT NULL
                );
            END;

            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'stack_flashcards') AND type in (N'U'))
            BEGIN
                CREATE TABLE stack_flashcards (
                    StackId INT,
                    FlashcardId INT,
                    PRIMARY KEY (StackId, FlashcardId),
                    FOREIGN KEY (StackId) REFERENCES stacks(Id) ON DELETE CASCADE,
                    FOREIGN KEY (FlashcardId) REFERENCES flashcards(Id) ON DELETE CASCADE
                );
            END;";

        connection.Execute(createTables);

        // string sql = "SELECT COUNT(*) FROM coding";
        // int count = connection.ExecuteScalar<int>(sql);
        // if (count == 0)
        //     PopulateDatabase();

    }
}
