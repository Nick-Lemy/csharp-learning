namespace Library.Models;

public class Book
{
    static int Count { get; set; } = 1;
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }

    public bool IsBorrowed { get; set; }
    public Book(string title, string author)
    {
        Title = title;
        Author = author;
        IsBorrowed = false; ;
        Id = Count++;
    }

}
