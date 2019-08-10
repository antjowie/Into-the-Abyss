using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Temp ref to a player controller
    [SerializeField] Transform target;
    [SerializeField] Gun gun;
    [SerializeField] float speed = 2f;

    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.position - transform.position;
        Vector2 position = transform.position;

        rb.MovePosition(position + direction * speed * Time.deltaTime);

        // Update weapon
        gun.Aim(target.position);
    }
}
