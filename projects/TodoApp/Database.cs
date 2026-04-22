using Microsoft.Data.SqlClient;

namespace TodoProject;

class Database
{
    private static readonly string connectionString = "Host=localhost;Port=5432;Username=postgres;Database=todo_app;";

    public static void GetTodos()
    {
        using SqlConnection conn = new(connectionString);
        try
        {
            conn.Open();
            Console.WriteLine("Connection successful!");

            string query = "SELECT * FROM Todos";

            SqlCommand cmd = new(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public static void AddTodo()
    {
        using SqlConnection conn = new(connectionString);
        try
        {
            conn.Open();
            Console.WriteLine("Connection successful!");
            string query = "INSERT INTO todos (Name) VALUES (@name)";

            SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@name", "Nick");
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public static void DeleteTodo(int id)
    {
        using SqlConnection conn = new(connectionString);
        try
        {
            string query = "DELETE FROM todos WHERE id = @id";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"Deleted rows: {rowsAffected}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    public static void ToggleStatus(int id, bool newStatus)
    {
        using SqlConnection conn = new(connectionString);
        try
        {
            string query = "UPDATE todos SET status = @status WHERE id = @id";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@status", newStatus);
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"Rows updated: {rowsAffected}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}