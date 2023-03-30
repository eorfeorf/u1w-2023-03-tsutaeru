public class Note
{
    public NoteType Type;
    public float Time;
    public bool IsActive;
    public int Uid;

    public Note()
    {
        
    }
    
    public Note(Note note)
    {
        Type = note.Type;
        Time = note.Time;
        IsActive = note.IsActive;
        Uid = note.Uid;
    }
}