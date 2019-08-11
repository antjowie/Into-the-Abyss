using UnityEngine;

public class Skeleton : EnemyController
{
    [SerializeField] float distanceFromTarget = 10f;
    [SerializeField] float distanceFromTargetBias = 1f;
    
    Vector2 moveDirection;

    Transform targetPos;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        targetPos = GameObject.Find("Player").transform;
    }

    // Display the range inbetween what the enemy will try to stay
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceFromTarget - distanceFromTargetBias);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceFromTarget + distanceFromTargetBias);
    }

    void Update()
    {
        gun.Aim(targetPos.position);

        Vector2 distance = rb.transform.position - targetPos.position;

        // Decide move direction
        // Player is too close to enemy, enemy wants to walk away
        if(distance.magnitude < distanceFromTarget - distanceFromTargetBias)
        {
            moveDirection = rb.transform.position - targetPos.position;
        }
        else if (distance.magnitude > distanceFromTarget + distanceFromTargetBias)
        {
            moveDirection = targetPos.position - rb.transform.position;
        }
        else
        {
            moveDirection = Vector2.zero;
        }

        moveDirection.Normalize();
    }

    void FixedUpdate()
    {
        Vector2 position = rb.transform.position;
        
        rb.MovePosition(position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
