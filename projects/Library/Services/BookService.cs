using Library.Interfaces;
using Library.Models;

namespace Library.Services;

public class BookService : IBookService
{
    private List<Book> books = [];

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void BorrowBook(int id)
    {
        Book? book = books.Find(book => book.Id == id);
        if (book == null) Console.WriteLine("The Book is not available!");
        else book.IsBorrowed = true;
    }

    public void ReturnBook(int id)
    {
        Book? book = books.Find(book => book.Id == id);
        if (book == null) Console.WriteLine("The Book is not available!");
        else book.IsBorrowed = false;
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }
}
