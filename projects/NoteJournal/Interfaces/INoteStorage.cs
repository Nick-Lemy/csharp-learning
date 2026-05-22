using NoteJournal.Models;

namespace NoteJournal.interfaces;

public interface INoteStorage
{
    Task SaveNoteAsync(Note note);
    Task<Note?> LoadNoteAsync(int id);
    void DeleteNote(int id);
    Task<List<int>> GetAllNoteIdsAsync();
}
