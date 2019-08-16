using UnityEngine;
using UnityEngine.SceneManagement;

// Player controller is responsible for handling input that is given to the player
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Sword weapon = null;

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

    public enum PlayerState
    {
        Spawning,
        Idle,
        Moving,
        Dashing,
        Dead
    }

    public PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = PlayerState.Spawning;
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
        switch (state)
        {
            case PlayerState.Spawning:
                state = PlayerState.Idle;
                break;

            case PlayerState.Idle:
                if(isDashing && isMoving)
                {
                    state = PlayerState.Dashing;
                } 
                else if (isMoving)
                {
                    state = PlayerState.Moving;
                }
                break;

            case PlayerState.Moving:
                if(!isMoving)
                {
                    state = PlayerState.Idle;
                }
                else if (isDashing)
                {
                    state = PlayerState.Dashing;
                }
                break;

            case PlayerState.Dashing:
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
                        state = PlayerState.Moving;
                    }
                    else
                    {
                        state = PlayerState.Idle;
                    }

                }
                break;

            case PlayerState.Dead:
                // TODO: add death animation
                Destroy(gameObject);
                break;
        }
    }

    void FixedUpdate()
    {
        // Execute movement related stuff for the fsm
        switch (state)
        {
            case PlayerState.Spawning:
                break;

            case PlayerState.Idle:
                break;

            case PlayerState.Moving:
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                break;

            case PlayerState.Dashing:
                rb.MoveRotation(dashRotation);
                rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
                break;

            case PlayerState.Dead:
                break;
        }
    }
}
