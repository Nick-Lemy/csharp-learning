static class TodoApp
{
    static int todoId = 0;
    static readonly List<Task> todolist = [];

    static void AddNewTask(string title, string desc)
    {
        Task newTask = new(title, desc, id:++todoId);
        todolist.Add(newTask);
        Console.WriteLine($"New Task: \"{newTask.Title}\" was added successfully!");
    }

    static void DisplayTodoList()
    {
        Console.WriteLine("List of Todos:");
        foreach(Task task in todolist)
        {
            Console.WriteLine(task);
        }
    }
    static void ModifyStatus(int id)
    {
        var task = FindOneTask(id);
        task.Status = !task.Status;

    }
    static void UpdateTodo(int id, string? title, string? description)
    {
        Task task = FindOneTask(id);
        task.Title = title ?? task.Title;
        task.Description = description ?? task.Description;
    }

    static private Task FindOneTask(int id)
    {
        var task = todolist.First(task => task.Id == id) ?? throw new Exception("Task not found!");
        return task;
    }

    static void RemoveTodo(int id)
    {
        var taskIndex = todolist.FindIndex(task => task.Id == id);
        todolist.RemoveAt(taskIndex);
        Console.WriteLine($"Task No{id} Removed Successfully!");
    }

    static void ClearTodos()
    {
        todolist.Clear();
        Console.WriteLine("Todo list cleared!");
    }
}