using Library.Models;

namespace Library.Interfaces;

public interface IBookService
{
    List<Book> GetAllBooks();
    void BorrowBook(int id);
    void ReturnBook(int id);
    void AddBook(Book book);
}
