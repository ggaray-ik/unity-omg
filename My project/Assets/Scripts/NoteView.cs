using UnityEngine;

public class NoteView : MonoBehaviour
{
    int lane;
    float targetHitTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up;
    }
}
