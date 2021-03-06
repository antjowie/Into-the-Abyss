﻿using UnityEngine;

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
        // Aim and shoot the gun
        gun.Aim(targetPos.position);
        Vector2 lookDir = ((Vector2)targetPos.position - rb.position).normalized;

        // We need to only check walls and players. These are layer 10 and 11
        int layermask = (1 << 10) + (1 << 11);
        
        // We use lookdir but we should multiply it by the size of the enemy so that the raycast doesn't hit itself
        RaycastHit2D hit = Physics2D.Raycast(rb.position + lookDir, lookDir, 100000f, layermask);
        if (hit.collider.name == "Player")
        {
            gun.Shoot();
            Debug.DrawRay(rb.position + lookDir, hit.point - rb.position - lookDir,Color.red);
        }
        else
        {
            Debug.Log(hit.collider.name);
            Debug.DrawRay(rb.position + lookDir,hit.point - rb.position - lookDir);
        }

        // Decide move direction
        // Player is too close to enemy, enemy wants to walk away
        Vector2 distance = rb.transform.position - targetPos.position;
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
