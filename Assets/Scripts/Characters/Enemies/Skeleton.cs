using UnityEngine;

public class Skeleton : EnemyController, IMovableEnemy
{
    [SerializeField] float distanceFromTarget = 10f;
    [SerializeField] float distanceFromTargetBias = 1f;

    Vector2 moveDirection;

    Transform targetPos;
    Rigidbody2D rb;

    protected override void OnStart()
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

    protected override void OnUpdate()
    {
        // Aim and shoot the gun
        gun.Aim(targetPos.position);
        Vector2 lookDir = ((Vector2)targetPos.position - rb.position).normalized;

        // We use lookdir but we should multiply it by the size of the enemy so that the raycast doesn't hit itself
        RaycastHit2D hit = Physics2D.Raycast(rb.position + lookDir, lookDir);
        if (hit.collider.name == "Player")
        {
            gun.Shoot();
            Debug.DrawRay(rb.position + lookDir, hit.point - rb.position, Color.red);
        }
        else
        {
            Debug.DrawRay(rb.position + lookDir, hit.point - rb.position);
        }

        // Decide move direction
        // Player is too close to enemy, enemy wants to walk away
        Vector2 distance = rb.transform.position - targetPos.position;
        if (distance.magnitude < distanceFromTarget - distanceFromTargetBias)
        {
            moveDirection = rb.transform.position - targetPos.position;
        }
        else
        {
            moveDirection = Vector2.zero;
        }

        moveDirection.Normalize();
    }

    protected override void OnFixedUpdate()
    {
        Vector2 position = rb.transform.position;

        rb.MovePosition(position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void ChangeMoveDirection(Vector2 newMoveDirection)
    {
        moveDirection = newMoveDirection;
    }

    protected override void HandleSwitchingState(EnemyStateType stateToSwitchTo)
    {
        switch (stateToSwitchTo) {
            case EnemyStateType.Chase:
                float catchUpDistance = distanceFromTarget + distanceFromTargetBias;
                currentEnemyState = new ChaseEnemyState(this, targetPos, catchUpDistance);
                break;
        }
    }
}
