using System;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    // One sorted list per lane tracker
    private List<NoteView>[] laneNotes = new List<NoteView>[4];

    [SerializeField] private float hitWindow = 0.08f;

    // Events to broadcast hit/miss results
    public static event Action<int> OnNoteHit;
    public static event Action<int> OnNoteMiss;

    private void Awake()
    {
        // Initialize the array of lists for the 4 lanes
        for (int i = 0; i < 4; i++)
        {
            laneNotes[i] = new List<NoteView>();
        }
    }

    private void OnEnable()
    {
        InputManager.OnLanePressed += HandleLanePressed;
    }

    private void OnDisable()
    {
        InputManager.OnLanePressed -= HandleLanePressed;
    }

    // Called by NoteSpawner immediately after instantiating a NoteView
    public void RegisterNote(NoteView note)
    {
        if (note != null && note.lane >= 0 && note.lane < 4)
        {
            laneNotes[note.lane].Add(note);
        }
    }

    // Called if a note goes safely past the bounds / gets destroyed natively
    public void UnregisterNote(NoteView note)
    {
        if (note != null && note.lane >= 0 && note.lane < 4)
        {
            laneNotes[note.lane].Remove(note);
        }
    }

    private void HandleLanePressed(int lane)
    {
        if (lane < 0 || lane >= 4) return;

        List<NoteView> notes = laneNotes[lane];

        // Clean up any notes that were destroyed outside the HitZone (e.g. they fell off screen)
        while (notes.Count > 0 && notes[0] == null)
        {
            notes.RemoveAt(0);
        }

        // Miss: list is empty (no note in lane to hit)
        if (notes.Count == 0)
        {
            OnNoteMiss?.Invoke(lane);
            return;
        }

        // Grab front note and calculate timing offset
        NoteView frontNote = notes[0];
        
        // Note: Make sure your NoteSpawner has `public static float songTime;`
        float timeDiff = Mathf.Abs(NoteSpawner.songTime - frontNote.targetHitTime);

        if (timeDiff <= hitWindow)
        {
            // Hit! Remove from list, broadcast, and destroy
            notes.RemoveAt(0);
            OnNoteHit?.Invoke(lane);
            Destroy(frontNote.gameObject);
        }
        else
        {
            // Miss: note exists but was pressed too early/late 
            OnNoteMiss?.Invoke(lane);
        }
    }
}