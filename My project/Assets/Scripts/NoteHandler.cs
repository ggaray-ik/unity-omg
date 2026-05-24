using UnityEngine;

public class NoteHandler : MonoBehaviour
{
    [Header("References")]
    private NoteSpawner noteSpawnerRef;
    private TimingHandler timingHandlerRef;

    public static NoteSpawner noteSpawner;
    public static TimingHandler timingHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        noteSpawner = noteSpawnerRef;
        timingHandler = timingHandlerRef;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void deleteNote(NoteView note)
    {
        timingHandler.ActiveNotes.Remove(note);
        Destroy(note.gameObject);
    }
}
