using Flashcards.harris_andy;
using Microsoft.Data.SqlClient;
using Dapper;

internal class Program
{
    private static void Main(string[] args)
    {
        UseDB.InitializeDatabase();
    }
}
