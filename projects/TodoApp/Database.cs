using Npgsql;
namespace TodoProject;

class Database
{

    private static readonly string connectionString = "Host=localhost;Port=5432;Username=postgres;Database=todo_app;";
    private static readonly NpgsqlConnection conn = new(connectionString);

    public static void GetTodos()
    {
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
                string status = (bool)reader["status"] ? "Completed" : "Pending";
                Console.WriteLine($"id: {reader["id"]}, title: {reader["title"]}, desc: {reader["description"]}, status: {status}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            conn.Close();
        }
    }

    public static Todo? GetOneTodo(int id)
    {
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
        finally
        {
            conn.Close();
        }
    }
    public static void AddTodo(Todo todo)
    {
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
        conn.Close();
    }

    public static void DeleteTodo(int id)
    {
        try
        {
            conn.Open();
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
        finally
        {
            conn.Close();
        }
    }

    public static void ChangeStatus(int id, bool newStatus)
    {
        try
        {
            conn.Open();
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
        finally
        {
            conn.Close();
        }
    }
}