using UnityEngine;
using System.Collections.Generic;

public class TimingHandler : MonoBehaviour
{
    public List<NoteView> ActiveNotes = new List<NoteView>();
    [SerializeField] private float errorMargin = 0.08f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (NoteView note in ActiveNotes)
        {
            if (Mathf.Abs(note.targetHitTime - NoteSpawner.songTime) < errorMargin)
            {
                // Accessing properties as requested
                int currentLane = note.lane;
                float currentTargetTime = note.targetHitTime; 
            }
        }
    }

    public void RegisterNote(NoteView note)
    {
        ActiveNotes.Add(note);
        note.transform.SetParent(this.transform);  // Para mantener la jerarquía organizada
    }
}
