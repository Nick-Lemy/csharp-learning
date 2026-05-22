using NoteJournal.interfaces;
using NoteJournal.Models;

namespace NoteJournal.Services;

public class Journal
{
    private readonly INoteStorage _storage;

    public event Action<Note>? OnNoteSaved;
    public event Action<int>? OnNoteDeleted;

    public Journal(INoteStorage storage)
    {
        _storage = storage;
    }

    public async Task SaveNoteAsync(string title, string content)
    {
        Note note = new(title, content);
        await _storage.SaveNoteAsync(note);
        OnNoteSaved?.Invoke(note);
    }

    public void DeleteNote(int id)
    {
        _storage.DeleteNote(id);
        OnNoteDeleted?.Invoke(id);
    }

    public async Task<List<Note>> GetAllNotesAsync()
    {
        List<int> ids = await _storage.GetAllNoteIdsAsync();
        List<Note> notes = [];
        foreach (int id in ids)
        {
            Note? note = await _storage.LoadNoteAsync(id);
            if (note != null) notes.Add(note);
        }
        return notes;
    }

    public IEnumerable<Note> GetPage(List<Note> notes, int pageSize, int pageNumber)
    {
        int start = (pageNumber - 1) * pageSize;
        for (int i = start; i < Math.Min(start + pageSize, notes.Count); i++)
            yield return notes[i];
    }

    public void DisplayNote(Note note, Func<Note, string> formatter)
    {
        Console.WriteLine(formatter(note));
    }
}
