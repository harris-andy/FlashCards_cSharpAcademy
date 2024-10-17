using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace Flashcards.harris_andy
{
    public class AppConfig
    {
        // public static string ConnectionString => ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["connectionString"]?.ConnectionString ?? throw new Exception("Connection string not found.");
        public static string dbPath = ConfigurationManager.AppSettings["DB-Path"] ?? "./";
    }
}