class Student(string name)
{
    private int _score = 0;
    public string Name
    { get; set; } = name;
    public int Score
    {
        get => _score; set
        {
            if (value < 0 || value > 100)
            {
                _score = 0;
            }
            else
            {
                _score = 0;
            }
        }
    }

    public string GetGrade()
    {
        string grade = Score switch
        {
            > 89 => "A",
            > 79 => "B",
            > 69 => "C",
            > 60 => "D",
            _ => "F"
        };
        return $"{Name} scored {Score} => {grade}";
    }

}