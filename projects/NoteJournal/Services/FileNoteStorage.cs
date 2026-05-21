using NoteJournal.interfaces;
using NoteJournal.Models;

namespace NoteJournal.Services;

public class FileNoteStorage : INoteStorage
{
    public async Task SaveNoteAsync(Note note)
    {
        string filePath = GetFilePath(note.Id);
        string content = $"Title: {note.Title}\nContent: {note.Content}\nCreatedAt: {note.CreatedAt}";
        try
        {
            await File.WriteAllTextAsync(filePath, content);
            Console.WriteLine($"Note with ID {note.Id} saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving note with ID {note.Id}: {ex.Message}");
        }

    }

    public async Task<Note?> LoadNoteAsync(int id)
    {
        string filePath = GetFilePath(id);
        if (!File.Exists(filePath)) return null;
        string content = await File.ReadAllTextAsync(filePath);
        var lines = content.Split('\n');
        string title = lines[0].Replace("Title: ", "");
        string noteContent = lines[1].Replace("Content: ", "");
        DateTimeOffset createdAt = DateTimeOffset.Parse(lines[2].Replace("CreatedAt: ", ""));

        return new Note(title, noteContent) { Id = id, CreatedAt = createdAt };
    }

    public void DeleteNoteAsync(int id)
    {
        string filePath = GetFilePath(id);
        if (!File.Exists(filePath)) Console.WriteLine($"Note with ID {id} not found.");
        else
        {
            File.Delete(filePath);
            Console.WriteLine($"Note with ID {id} deleted.");
        }
    }

    public async Task<List<int>> GetAllNoteIdsAsync()
    {
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "note_*.txt");
        return files.Select(f => int.Parse(Path.GetFileNameWithoutExtension(f).Replace("note_", ""))).ToList();
    }


    static private string GetFilePath(int id) => $"note_{id}.txt";
}