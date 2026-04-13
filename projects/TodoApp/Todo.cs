namespace TodoProject;

public class Todo(string title, string description, int id)
{
    public int Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public bool Status { get; set; } = false;

    public override string ToString()
    {
        string statusString = Status ? "Completed" : "Pending";
        return $"No{Id} - Title: {Title}; Desc: {Description}; Status: {statusString} ";
    }
}
