using NoteJournal.Services;

var storage = new FileNoteStorage();
var journal = new Journal(storage);
var logger = new Logger();

journal.OnNoteSaved += logger.OnNoteSaved;
journal.OnNoteDeleted += logger.OnNoteDeleted;

while (true)
{
    Console.WriteLine("\nNote Journal");
    Console.WriteLine("1. Add note");
    Console.WriteLine("2. List notes");
    Console.WriteLine("3. Read note");
    Console.WriteLine("4. Delete note");
    Console.WriteLine("5. Exit");
    Console.Write("\nSelect an option: ");

    if (!int.TryParse(Console.ReadLine(), out int option))
    {
        Console.WriteLine("Invalid input.");
        continue;
    }

    switch (option)
    {
        case 1:
            Console.Write("Title: ");
            string? title = Console.ReadLine();
            Console.Write("Content: ");
            string? content = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Title and content cannot be empty.");
                break;
            }
            await journal.SaveNoteAsync(title, content);
            break;

        case 2:
            var notes = await journal.GetAllNotesAsync();
            if (notes.Count == 0)
            {
                Console.WriteLine("No notes yet.");
                break;
            }

            Console.Write("Page number: ");
            if (!int.TryParse(Console.ReadLine(), out int page)) page = 1;

            foreach (var note in journal.GetPage(notes, pageSize: 3, pageNumber: page))
                journal.DisplayNote(note, n => $"[{n.Id}] {n.Title} — {n.CreatedAt:yyyy-MM-dd HH:mm}");
            break;

        case 3:
            Console.Write("Note ID: ");
            if (!int.TryParse(Console.ReadLine(), out int readId))
            {
                Console.WriteLine("Invalid ID.");
                break;
            }

            var loaded = await storage.LoadNoteAsync(readId);
            if (loaded == null)
            {
                Console.WriteLine("Note not found.");
                break;
            }

            journal.DisplayNote(loaded, n => $"\n[{n.Id}] {n.Title}\n{n.CreatedAt:yyyy-MM-dd HH:mm}\n\n{n.Content}\n");
            break;

        case 4:
            Console.Write("Note ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int deleteId))
            {
                Console.WriteLine("Invalid ID.");
                break;
            }

            journal.DeleteNote(deleteId);
            break;

        case 5:
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}
