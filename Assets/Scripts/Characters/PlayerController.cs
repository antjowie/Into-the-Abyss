using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    Rigidbody2D rb;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("HorizontalMove");
        movement.y = Input.GetAxisRaw("VerticalMove");

        movement.Normalize();

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
