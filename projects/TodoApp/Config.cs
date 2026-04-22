namespace TodoProject;

using dotenv.net;

public static class Config
{
    public static readonly string connectionString;

    static Config()
    {
        DotEnv.Load();
        string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        string dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
        string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
        string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "password";
        string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "todo_app";
        connectionString = $"Host={dbHost};Port={dbPort};Username={dbUser};Database={dbName};Password={dbPassword}";
    }
}