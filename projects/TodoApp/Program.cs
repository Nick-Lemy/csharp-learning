using TodoProject;

TodoApp.RunApp();
if (!int.TryParse(Console.ReadLine(), out int option))
{
    Console.WriteLine("Invalid input! Please enter a number corresponding to the options.");
    TodoApp.RunApp();
}

Console.WriteLine(option);