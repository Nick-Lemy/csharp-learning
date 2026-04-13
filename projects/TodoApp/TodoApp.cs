namespace TodoProject
{
    static public class TodoApp
    {
        private static int todoId = 0;
        private static readonly List<Todo> todolist = [];

        public static void AddNewTodo(string title, string desc)
        {
            Todo newTask = new(title, desc, id: ++todoId);
            todolist.Add(newTask);
            Console.WriteLine($"\nNew Task: \"{newTask.Title}\" was added successfully!");
        }
        public static void DisplayTodoList()
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
        public static void ToggleTodoStatus(int id)
        {
            var task = FindOneTodo(id);
            task.Status = !task.Status;
            Console.WriteLine($"Status of Task No{id}, changed to {task.Status}");
        }
        public static void UpdateTodo(int id, string? title, string? description)
        {
            Todo task = FindOneTodo(id);
            task.Title = title ?? task.Title;
            task.Description = description ?? task.Description;
            Console.WriteLine(task.ToString());
            Console.WriteLine($"Task No{id} updated sucessfully!");
        }
        public static void RemoveTodo(int id)
        {
            var taskIndex = todolist.FindIndex(task => task.Id == id);
            todolist.RemoveAt(taskIndex);
            Console.WriteLine($"\nTask No{id} removed successfully!");
        }
        public static void ClearTodos()
        {
            todolist.Clear();
            Console.WriteLine("\nTodo list cleared!");
        }

        static private Todo FindOneTodo(int id)
        {
            var task = todolist.First(task => task.Id == id) ?? throw new Exception("Task not found!");
            return task;
        }

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

        }

    }
}