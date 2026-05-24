using UnityEngine;
using System.Collections.Generic;


public class NoteSpawner : MonoBehaviour
{
    public static float songTime = 0f;

    [System.Serializable]
    public struct NoteData
    {
        public int  lane;
        public float hitTime;
    }

    [Header("References")]
    public GameObject[] notePrefabs;
    public Vector3[] laneSpawnPoints;   // 4 transforms, uno por carril

    [Header("Timing")]
    public float travelTime = 2f;         // segundos que tarda la nota en llegar al hit line

    // ── Hardcoded test chart ────────────────────────────────────────────
    private readonly List<NoteData> chart = new List<NoteData>
    {
        // Intro — beats simples
        // new NoteData { lane = 0, hitTime =  0.50f },
        // new NoteData { lane = 2, hitTime =  1.00f },
        new NoteData { lane = 1, hitTime =  1.50f },
        new NoteData { lane = 3, hitTime =  2.00f },
        new NoteData { lane = 0, hitTime =  2.50f },
        new NoteData { lane = 2, hitTime =  3.00f },

        // Build — corcheas
        new NoteData { lane = 1, hitTime =  8.00f },
        new NoteData { lane = 3, hitTime =  8.25f },
        new NoteData { lane = 0, hitTime =  8.50f },
        new NoteData { lane = 2, hitTime =  8.75f },
        new NoteData { lane = 1, hitTime =  9.00f },
        new NoteData { lane = 3, hitTime =  9.50f },
        new NoteData { lane = 0, hitTime = 10.00f },
        new NoteData { lane = 2, hitTime = 10.25f },
        new NoteData { lane = 1, hitTime = 10.50f },
        new NoteData { lane = 3, hitTime = 10.75f },

        // Drop — chords + síncopa
        new NoteData { lane = 0, hitTime = 16.00f },
        new NoteData { lane = 2, hitTime = 16.00f },  // chord simultáneo
        new NoteData { lane = 1, hitTime = 16.75f },
        new NoteData { lane = 3, hitTime = 17.00f },
        new NoteData { lane = 0, hitTime = 17.50f },
        new NoteData { lane = 1, hitTime = 17.50f },  // chord simultáneo
        new NoteData { lane = 2, hitTime = 18.25f },
        new NoteData { lane = 3, hitTime = 18.50f },
        new NoteData { lane = 0, hitTime = 19.00f },
        new NoteData { lane = 2, hitTime = 19.25f },

        // Outro — disperso
        new NoteData { lane = 1, hitTime = 24.00f },
        new NoteData { lane = 3, hitTime = 25.00f },
        new NoteData { lane = 0, hitTime = 26.50f },
        new NoteData { lane = 2, hitTime = 28.00f },
        new NoteData { lane = 1, hitTime = 29.50f },
    };
    // ────────────────────────────────────────────────────────────────────
    private int nextNoteIndex;

    void Start()
    {
        // Reset static songTime so replaying works correctly
        songTime = 0f;
        nextNoteIndex = 0;
    }
    void Update()
    {
        songTime = getDummySongTime();
        while (nextNoteIndex < chart.Count &&
               chart[nextNoteIndex].hitTime - songTime < travelTime)
        {
            spawnNote(chart[nextNoteIndex]);
            
            nextNoteIndex++;
        }
    }

    float getDummySongTime()
    {
        return (songTime + Time.deltaTime);
    }
    
    void spawnNote(NoteData data)
    {
        Vector3 spawnPoint = laneSpawnPoints[data.lane];
        GameObject note = Instantiate(notePrefabs[data.lane], spawnPoint, Quaternion.identity);

        note.GetComponent<NoteView>().Init(data.lane, data.hitTime, travelTime, spawnPoint.y);
    }
}
