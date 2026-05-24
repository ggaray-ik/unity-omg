using UnityEngine;
using System.Collections.Generic;

public class TimingHandler : MonoBehaviour
{
    public List<NoteView> ActiveNotes = new List<NoteView>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterNote(NoteView note)
    {
        ActiveNotes.Add(note);
        note.transform.SetParent(this.transform);  // Para mantener la jerarquía organizada
    }
}
