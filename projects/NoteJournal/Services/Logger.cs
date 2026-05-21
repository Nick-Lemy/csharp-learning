using NoteJournal.Models;

namespace NoteJournal.Services;

public class Logger
{
    public void OnNoteSaved(Note note) =>
        Console.WriteLine($"[LOG] Note \"{note.Title}\" saved at {note.CreatedAt:HH:mm:ss}");

    public void OnNoteDeleted(int id) =>
        Console.WriteLine($"[LOG] Note #{id} deleted");
}
