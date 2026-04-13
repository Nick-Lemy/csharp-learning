using TodoProject;

internal class Program
{
    private static void Main(string[] args)
    {
        TodoApp.RunApp();
        if (!int.TryParse(Console.ReadLine(), out int option))
        {
            Console.WriteLine("Invalid input! Please enter a number corresponding to the options.");
            Main([]);
        }

        switch (option)
        {
            case 1:
                Console.Write("Enter new task's title: ");
                string? title = Console.ReadLine();

                Console.Write("Enter new task's description: ");
                string? desc = Console.ReadLine();

                if (title == null || desc == null)
                {
                    Console.WriteLine("Invalid input, please enter a valid title or description");
                    Main([]);
                    break;
                }
                TodoApp.AddNewTodo(title, desc);
                Main([]);
                break;
            case 2:
                TodoApp.DisplayTodoList();
                Main([]);
                break;

            case 3:
                Console.Write("Enter id of the Task to toggle status: ");
                if (int.TryParse(Console.ReadLine(), out int idOfTaskToToggleStatus))
                {
                    Console.WriteLine("Invalid input");
                    Main([]);
                    break;
                }
                TodoApp.ToggleTodoStatus(idOfTaskToToggleStatus);
                break;
            case 4:
                Console.Write("Enter id of the Task to update: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid input");
                    Main([]);
                    break;
                }

                Console.WriteLine("Enter updated title: ");
                string? updatedTitle = Console.ReadLine();

                Console.WriteLine("Enter updated description: ");
                string? updatedDescription = Console.ReadLine();
                TodoApp.UpdateTodo(id, description: updatedDescription?.Length <= 1 ? null : updatedDescription, title: updatedTitle?.Length <= 1 ? null : updatedTitle);
                break;
            case 5:
                Console.Write("Enter id of the Task to update: ");
                if (int.TryParse(Console.ReadLine(), out int idToRemove))
                {
                    Console.WriteLine("Invalid input");
                    Main([]);
                    break;
                }
                TodoApp.RemoveTodo(idToRemove);
                break;
            case 6:
                TodoApp.ClearTodos();
                break;
            case 7:
                break;
            default:
                Main([]);
                break;
        }
    }
}