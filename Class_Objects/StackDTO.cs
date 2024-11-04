namespace Flashcards.harris_andy;

public class StackDTO
{
    public int Id { get; set; }
    public string Name { set; get; }
    public int SessionCount { set; get; }

    public StackDTO(int id, string name, int sessions)
    {
        Id = id;
        Name = name;
        SessionCount = sessions;
    }
}