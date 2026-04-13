namespace TodoProject
{
    static public class TodoApp
    {
        private static int todoId = 0;
        private static readonly List<Todo> todolist = [];

        public static void AddNewTask(string title, string desc)
        {
            Console.WriteLine("");
            Todo newTask = new(title, desc, id: ++todoId);
            todolist.Add(newTask);
            Console.WriteLine($"New Task: \"{newTask.Title}\" was added successfully!");
        }
        public static void DisplayTodoList()
        {
            Console.WriteLine("List of Todos:");
            foreach (Todo task in todolist)
            {
                Console.WriteLine(task);
            }
        }
        public static void ModifyStatus(int id)
        {
            var task = FindOneTask(id);
            task.Status = !task.Status;

        }
        public static void UpdateTodo(int id, string? title, string? description)
        {
            Todo task = FindOneTask(id);
            task.Title = title ?? task.Title;
            task.Description = description ?? task.Description;
        }
        public static void RemoveTodo(int id)
        {
            var taskIndex = todolist.FindIndex(task => task.Id == id);
            todolist.RemoveAt(taskIndex);
            Console.WriteLine($"Task No{id} Removed Successfully!");
        }
        public static void ClearTodos()
        {
            todolist.Clear();
            Console.WriteLine("Todo list cleared!");
        }

        static private Todo FindOneTask(int id)
        {
            var task = todolist.First(task => task.Id == id) ?? throw new Exception("Task not found!");
            return task;
        }

        public static void RunApp()
        {
            Console.WriteLine("====================== Welcome to the Todo App ======================");
            Console.WriteLine("Available Options: ");
            Console.WriteLine("1. Add a new task");
            Console.WriteLine("2. Display the todo list");
            Console.WriteLine("3. Toggle the status of a task");
            Console.WriteLine("4. Update a task");
            Console.WriteLine("5. Remove a task");
            Console.WriteLine("6. Clear the todo list");
            Console.WriteLine("7. Exit\n");

            Console.Write("Select un number between 1-7: ");

        }

    }
}