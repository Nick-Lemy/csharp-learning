namespace TodoProject;

static public class TodoApp
{
    private static int todoId = 0;
    private static readonly List<Todo> todolist = [];

    public static void RunApp()
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

        Console.Write("Select un number between 1-7: ");

        if (!int.TryParse(Console.ReadLine(), out int option))
        {
            Console.WriteLine("Invalid input! Please enter a number corresponding to the options.");
            RunApp();
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
                break;
        }
        RunApp();

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
        UpdateTodo(id, description: updatedDescription?.Length <= 1 ? null : updatedDescription, title: updatedTitle?.Length <= 1 ? null : updatedTitle);

    }

    private static void OnOptionFive()
    {
        Console.Write("Enter id of the Task to update: ");
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
        todolist.Add(newTask);
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
        foreach (Todo task in todolist)
        {
            Console.WriteLine(task);
        }
    }
    private static void ToggleTodoStatus(int id)
    {
        var task = FindOneTodo(id);
        task.Status = !task.Status;
        Console.WriteLine(task.ToString());
        Console.WriteLine($"Status of Task No{id}, changed to {task.Status}");
    }
    private static void UpdateTodo(int id, string? title, string? description)
    {
        Todo task = FindOneTodo(id);
        task.Title = title ?? task.Title;
        task.Description = description ?? task.Description;
        Console.WriteLine(task.ToString());
        Console.WriteLine($"Task No{id} updated sucessfully!");
    }
    private static void RemoveTodo(int id)
    {
        var taskIndex = todolist.FindIndex(task => task.Id == id);
        todolist.RemoveAt(taskIndex);
        Console.WriteLine($"\nTask No{id} removed successfully!");
    }
    private static void ClearTodos()
    {
        todolist.Clear();
        Console.WriteLine("\nTodo list cleared!");
    }
    private static Todo FindOneTodo(int id)
    {
        var task = todolist.First(task => task.Id == id) ?? throw new Exception("Task not found!");
        return task;
    }



}