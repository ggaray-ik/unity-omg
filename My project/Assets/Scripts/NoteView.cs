using UnityEngine;

public class NoteView : MonoBehaviour
{
    [SerializeField] private Sprite pressedSprite; 
    [SerializeField] private Sprite unpressedSprite; 

    int lane;
    float targetHitTime;
    float travelTime;
    float spawnY;
    float hitY = -5f;

    public void Init(int lane, float targetHitTime, float travelTime, float spawnY)
    {
        this.lane = lane;
        this.targetHitTime = targetHitTime;
        this.travelTime = travelTime;
        this.spawnY = spawnY;
    }

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unpressedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        // How many seconds remain until this note should be hit
        float timeUntilHit = targetHitTime - NoteSpawner.songTime;

        float progress = 1f - (timeUntilHit / travelTime);

        float y = spawnY + (hitY-spawnY) * progress;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

        // Destroy the note once it's well past the hit line
        if (y < hitY - 3f)
        {
            Destroy(gameObject);
        }
    }
}
