using UnityEngine;

public class NoteView : MonoBehaviour
{
    [SerializeField] private Sprite pressedSprite; 
    [SerializeField] private Sprite unpressedSprite; 

    int lane;
    float noteSpeed;
    float targetHitTime;
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unpressedSprite;

        const int HarcodedLane = 1;
        const float hardcodedNoteSpeed = 1.0f;
        const float hardcodedTargetHitTime = 5.0f;
        lane = HarcodedLane;
        noteSpeed = hardcodedNoteSpeed;
        targetHitTime = hardcodedTargetHitTime;
    }

    // Update is called once per frame
    void Update()
    {
        float hardcodedHitPosition = -5f;
        float y = hardcodedHitPosition + (targetHitTime - NoteSpawner.songTime) *noteSpeed;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}

