namespace TodoProject;

static public class TodoApp
{
    private static int todoId = 0;
    private static readonly List<Todo> todolist = [];

    public static void RunApp()
    {
        while (true)
        {
            Console.WriteLine("\n====================== Welcome to the Todo App ======================");
            Console.WriteLine("Available Options: ");
            Console.WriteLine("1. Add a new task");
            Console.WriteLine("2. Display the todo list");
            Console.WriteLine("3. Toggle the status of a task");
            Console.WriteLine("4. Update a task");
            Console.WriteLine("5. Remove a task");
            Console.WriteLine("6. Clear the todo list");
            Console.WriteLine("7. Exit\n");

            Console.Write("Select a number between 1-7: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid input! Please enter a number corresponding to the options.");
                continue;
            }

            switch (option)
            {
                case 1:
                    OnOptionOne();
                    break;
                case 2:
                    OnOptionTwo();
                    break;

                case 3:
                    OnOptionThree();
                    break;
                case 4:
                    OnOptionFour();
                    break;
                case 5:
                    OnOptionFive();
                    break;
                case 6:
                    OnOptionSix();
                    break;
                case 7:
                    Console.WriteLine("\nThanks for using our App!....\n");
                    return;
                default:
                    Console.WriteLine("Invalid option! Please select a number between 1 and 7.");
                    break;
            }
        }
    }

    private static void OnOptionOne()
    {
        Console.Write("Enter new task's title: ");
        string? title = Console.ReadLine();

        Console.Write("Enter new task's description: ");
        string? desc = Console.ReadLine();

        if (title == null || desc == null)
        {
            Console.WriteLine("Invalid input, please enter a valid title or description");
            return;
        }
        AddNewTodo(title, desc);
    }
    private static void OnOptionTwo()
    {
        DisplayTodoList();
    }
    private static void OnOptionThree()
    {
        Console.Write("Enter id of the Task to toggle status: ");
        if (!int.TryParse(Console.ReadLine(), out int idOfTaskToToggleStatus))
        {
            Console.WriteLine("Invalid input");
            return;
        }
        ToggleTodoStatus(idOfTaskToToggleStatus);
    }
    private static void OnOptionFour()
    {
        Console.Write("Enter id of the Task to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid input");
            return;
        }

        Console.Write("Enter updated title: ");
        string? updatedTitle = Console.ReadLine();

        Console.Write("Enter updated description: ");
        string? updatedDescription = Console.ReadLine();
        UpdateTodo(id, description: string.IsNullOrWhiteSpace(updatedDescription) ? null : updatedDescription, title: string.IsNullOrWhiteSpace(updatedTitle) ? null : updatedTitle);

    }
    private static void OnOptionFive()
    {
        Console.Write("Enter id of the Task to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int idToRemove))
        {
            Console.WriteLine("Invalid input");
            return;
        }
        RemoveTodo(idToRemove);
    }
    private static void OnOptionSix()
    {
        ClearTodos();
    }


    private static void AddNewTodo(string title, string desc)
    {
        Todo newTask = new(title, desc, id: ++todoId);
        Database.AddTodo(newTask);
        Console.WriteLine($"\nNew Task: \"{newTask.Title}\" was added successfully!");
    }
    private static void DisplayTodoList()
    {
        if (todolist.Count == 0)
        {
            Console.WriteLine("\nNo Todos!");
            return;
        }
        Console.WriteLine("\nList of Todos:");
        Database.GetTodos();
    }
    private static void ToggleTodoStatus(int id)
    {
        if (!FindOneTodo(id, out Todo task)) return;
        task.Status = !task.Status;
        Console.WriteLine(task.ToString());
        Console.WriteLine($"Status of Task No{id} updated successfully!");
    }
    private static void UpdateTodo(int id, string? title, string? description)
    {
        if (!FindOneTodo(id, out Todo task))
        {
            return;
        }
        task.Title = title ?? task.Title;
        task.Description = description ?? task.Description;
        Console.WriteLine(task.ToString());
        Console.WriteLine($"Task No{id} updated successfully!");
    }
    private static void RemoveTodo(int id)
    {
        var taskIndex = todolist.FindIndex(task => task.Id == id);
        if (taskIndex == -1)
        {
            Console.WriteLine("Task not found!");
            return;
        }
        todolist.RemoveAt(taskIndex);
        Console.WriteLine($"\nTask No{id} removed successfully!");
    }
    private static void ClearTodos()
    {
        todolist.Clear();
        Console.WriteLine("\nTodo list cleared!");
    }
    private static bool FindOneTodo(int id, out Todo todo)
    {
        Todo? task = todolist.FirstOrDefault(task => task.Id == id);
        if (task == null)
        {
            Console.WriteLine("Task not found!");
            todo = null!;
            return false;
        }
        todo = task;
        return true;
    }

}