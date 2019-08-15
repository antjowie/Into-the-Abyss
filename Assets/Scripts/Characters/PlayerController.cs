using UnityEngine;
using UnityEngine.SceneManagement;

// Player controller is responsible for handling input that is given to the player
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Sword weapon;

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

    InputDown horizontalAttack = new InputDown("HorizontalAttack");
    InputDown verticalAttack = new InputDown("VerticalAttack");
    InputDown dash = new InputDown("Dash");

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
    
    // Update the movement that the player requests
    void UpdateMovement()
    {
        movement.x = Input.GetAxisRaw("HorizontalMove"); 
        movement.y = Input.GetAxisRaw("VerticalMove");   

        movement.Normalize();
    }

    // Update the cooldown of the dash and checks whether the player is dashing
    void UpdateDashState()
    {
        // Update dash cooldown
        dashCooldownProgression = Mathf.Clamp(dashCooldownProgression - Time.deltaTime, 0, dashCooldown);
        if (!isDashing && dashCooldownProgression == 0f)
        {
            isDashing = dash.GetInputDown() == 1f;
        }
    }

    void UpdateAttackState()
    {
        float x = horizontalAttack.GetInputDown();
        float y = verticalAttack.GetInputDown();
        if (x != 0f)
            weapon.Attack(x == -1f ? Sword.Direction.Left : Sword.Direction.Right);
        else if (y != 0f)
            weapon.Attack(y == -1f ? Sword.Direction.Down: Sword.Direction.Up);
    }
    
    void Update()
    {
        // Update objects that are used for transitions is the fsm
        UpdateMovement();
        UpdateDashState();
        UpdateAttackState();
        bool isMoving = movement.sqrMagnitude != 0f;

        horizontalAttack.Update();
        verticalAttack.Update();
        dash.Update();

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
                // Initial enter condition
                if (dashProgression == 0f)
                    dashDirection = movement;

                // Update the progression of the dash
                dashProgression = Mathf.Clamp(dashProgression + Time.deltaTime, dashProgression, dashDuration);
                dashRotation = dashProgression / dashDuration * 360f;
                if (dashDirection.x > 0f)
                    dashRotation *= -1f;

                // Transform the object. This is a temp and will be replaced with an animation trigger
                transform.rotation = Quaternion.Euler(0, 0, dashRotation);

                // Exit condition
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
                rb.MoveRotation(dashRotation);
                rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
                break;

            case State.Dead:
                break;
        }
    }
}
