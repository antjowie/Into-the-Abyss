using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed { private set; get; }
    public GameObject fromObject { private set; get; }


    public void Launch(GameObject gameObject, Vector2 direction, float speed)
    {
        fromObject = gameObject;
        this.speed = speed;
        rb.velocity = direction.normalized * this.speed;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This is called when a player or enemy is hit. The bullet should not traverse through walls
        Hitable hitable = collision.gameObject.GetComponent<Hitable>();
        if (hitable)
        {
            hitable.OnHit(this);
        }
        else if(!collision.GetComponent<Sword>())
        {
            Destroy(gameObject);
        }
    }
}
