using Npgsql;
namespace TodoProject;

class Database
{

    private static readonly string connectionString = "Host=localhost;Port=5432;Username=postgres;Database=todo_app;";

    public static void GetTodos()
    {
        using NpgsqlConnection conn = new(connectionString);
        try
        {
            conn.Open();
            Console.WriteLine("Connection successful!");

            string query = "SELECT * FROM Todos";

            NpgsqlCommand cmd = new(query, conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            Todo[] todolist = [];
            while (reader.Read())
            {
                Console.WriteLine(reader["id"] + ", " + reader["title"] + ", " + reader["description"]);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public static Todo? GetOneTodo(int id)
    {
        using NpgsqlConnection conn = new(connectionString);
        try
        {
            conn.Open();
            string query = "SELECT * FROM todos WHERE id = @id";
            using NpgsqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Todo todo = new(
                    id: (int)reader["id"],
                    title: (string)reader["title"],
                    description: (string)reader["description"]
                )
                { Status = (bool)reader["status"] };
                return todo;
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            throw;
        }
    }
    public static void AddTodo(Todo todo)
    {
        using NpgsqlConnection conn = new(connectionString);
        try
        {
            conn.Open();
            Console.WriteLine("Connection successful!");
            string query = "INSERT INTO todos (id, title, description, status) VALUES (@id, @title, @description, @status)";

            NpgsqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@id", todo.Id);
            cmd.Parameters.AddWithValue("@title", todo.Title);
            cmd.Parameters.AddWithValue("@description", todo.Description);
            cmd.Parameters.AddWithValue("@status", todo.Status);
            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"Inserted rows: {rowsAffected}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            throw;
        }
    }

    public static void DeleteTodo(int id)
    {
        using NpgsqlConnection conn = new(connectionString);
        try
        {
            string query = "DELETE FROM todos WHERE id = @id";
            using NpgsqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"Deleted rows: {rowsAffected}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            throw;
        }
    }

    public static void ToggleStatus(int id, bool newStatus)
    {
        using NpgsqlConnection conn = new(connectionString);
        try
        {
            string query = "UPDATE todos SET status = @status WHERE id = @id";
            using NpgsqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@status", newStatus);
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"Rows updated: {rowsAffected}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            throw;
        }
    }
}