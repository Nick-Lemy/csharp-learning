# NoteJournal

A file-based note journal CLI app built to practice core C# concepts.

## Concepts covered

- **OOP & Interfaces:** `Note` model, `INoteStorage` interface implemented by `FileNoteStorage`
- **Async & File I/O:** notes are saved and loaded from `.txt` files using `async/await`
- **Events:** `Journal` fires `OnNoteSaved` and `OnNoteDeleted`; a `Logger` class subscribes to react to them
- **Delegates:** `Func<Note, string>` passed into `DisplayNote` so the caller controls formatting
- **Iterators:** `GetPage` uses `yield return` to stream notes one at a time for pagination

## Run

```bash
dotnet run
```
