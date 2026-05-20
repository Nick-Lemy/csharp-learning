using Library.Models;
using Library.Services;


Console.WriteLine("Welcome to The Library!");
bool status = true;
BookService bookService = new();

while (status)
{

    Console.WriteLine("1. Add Book\n2. List Books\n3. Borrow Books\n4. Return a Book\n5. Exit");
    Console.Write("Choose a number between 1 - 4: ");
    string? input = Console.ReadLine();


    switch (input)
    {
        case "1":
            Console.Write("Enter a title: ");
            string? title = Console.ReadLine();

            Console.Write("\nEnter name of the Author: ");
            string? author = Console.ReadLine();

            Book newBook = new(title!, author!);
            bookService.AddBook(newBook);
            break;

        case "2":
            List<Book> books = bookService.GetAllBooks();
            foreach (Book book in books) Console.WriteLine($"Title: {book.Title} | Author: {book.Author} | IsBorrowed: {book.IsBorrowed}");
            break;

        case "3":
            Console.Write("Enter book id: ");
            bool success = Int32.TryParse(Console.ReadLine(), out int id);
            if (!success)
            {
                Console.WriteLine("Invalid id");
                break;
            }
            bookService.BorrowBook(id);
            break;
        case "4":
            Console.Write("Enter book id: ");
            bool isParsed = Int32.TryParse(Console.ReadLine(), out int bookId);
            if (!isParsed)
            {
                Console.WriteLine("Invalid id");
                break;
            }
            bookService.ReturnBook(bookId);
            break;
        case "5":
            status = false;
            break;
        default:
            Console.WriteLine("Invalid option");
            break;

    }


}