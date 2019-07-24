using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float dashSpeed = 8f;
    [SerializeField] float dashDuration = 0.5f;
    [SerializeField] float dashCooldown = 2f;

    bool isDashing = false;
    float dashProgression = 0f;
    float dashCooldownProgression = 0f;
    float dashRotation = 0f;
    Vector2 dashDirection;

    Rigidbody2D rb;
    Vector2 movement;

    enum State
    {
        Spawning,
        Idle,
        Moving,
        Dashing,
        Dead
    }
    State m_state = State.Spawning;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Update objects that are used for transitions is the fsm
        movement.x = Input.GetAxisRaw("HorizontalMove");
        movement.y = Input.GetAxisRaw("VerticalMove");

        movement.Normalize();
        bool isMoving = movement.sqrMagnitude != 0f;

        dashCooldownProgression = Mathf.Clamp(dashCooldownProgression - Time.deltaTime, 0, dashCooldown);
        if (!isDashing && dashCooldownProgression == 0f)
        {
           isDashing = Input.GetAxisRaw("Dash") == 1f;
        }

        //Debug.Log(m_state);

        // Execute the fsm and the transitions
        switch (m_state)
        {
            case State.Spawning:
                m_state = State.Idle;
                break;

            case State.Idle:
                if(isDashing && isMoving)
                {
                    m_state = State.Dashing;
                } 
                else if (isMoving)
                {
                    m_state = State.Moving;
                }
                break;

            case State.Moving:
                if(!isMoving)
                {
                    m_state = State.Idle;
                }
                else if (isDashing)
                {
                    m_state = State.Dashing;
                }
                break;

            case State.Dashing:
                if (dashProgression == 0f)
                    dashDirection = movement;


                dashProgression = Mathf.Clamp(dashProgression + Time.deltaTime, dashProgression, dashDuration);
                dashRotation = dashProgression / dashDuration * 360f;
                if (dashDirection.x > 0f)
                    dashRotation *= -1f;

                transform.rotation = Quaternion.Euler(0, 0, dashRotation);

                if (dashProgression == dashDuration)
                {
                    isDashing = false;
                    dashProgression = 0f;
                    dashCooldownProgression = dashCooldown;
                    if(isMoving)
                    {
                        m_state = State.Moving;
                    }
                    else
                    {
                        m_state = State.Idle;
                    }

                }
                break;

            case State.Dead:
                break;
        }
    }

    void FixedUpdate()
    {
        // Execute movement related stuff for the fsm
        switch (m_state)
        {
            case State.Spawning:
                break;
            case State.Idle:
                break;
            case State.Moving:
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                break;
            case State.Dashing:
                
                rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
                break;
            case State.Dead:
                break;
        }
    }
}
