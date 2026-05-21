namespace NoteJournal.Models;

public class Note
{
    static int _idCounter = 1;
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Note(string title, string content)
    {
        Title = title;
        Content = content;
        CreatedAt = DateTimeOffset.UtcNow;
        Id = _idCounter++;
    }
}