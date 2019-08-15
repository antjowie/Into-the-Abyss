using UnityEngine;

// The base class for a spawner. It defines the spawn area and has
// functions that are called when it is time to spawn
public abstract class Spawner : MonoBehaviour
{
    private bool shouldSpawn = false;
    private BoxCollider2D area;

    protected abstract void OnPlayerEnter();
    // This is only called if the player has entered the spawn area
    protected abstract void OnUpdate();

    // Assumes that a boxcollider is used
    protected Vector2 GetPointInArea()
    {
        Bounds bounds = area.bounds;
        Vector2 point = new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));
        
        return point;
    }

    private void Start()
    {
        area = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player enters spawn area
        if(collision.GetComponent<PlayerController>())
        {
            shouldSpawn = true;
            OnPlayerEnter();
        }
    }

    private void Update()
    {
        if (shouldSpawn)
            OnUpdate();
    }

}