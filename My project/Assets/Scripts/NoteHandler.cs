using UnityEngine;

public class NoteHandler : MonoBehaviour
{
    [Header("References")]
    public NoteSpawner noteSpawnerRef;
    public TimingHandler timingHandlerRef;

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
}
