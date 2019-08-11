using UnityEngine;

public class Sword : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };

    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float attackDuration = 0.25f;
    
    float attackProgression = 0f;
    float attackRegeneration = 0f; // If this value is zero, can attack again
    bool isAttacking = false;
    
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1f / attackDuration;
    }

    // Update is called once per frame
    void Update()
    {
        attackRegeneration = Mathf.Clamp(attackRegeneration - Time.deltaTime, 0, attackRegeneration);

        if (isAttacking)
        {
            attackProgression = Mathf.Clamp(attackProgression + Time.deltaTime, 0, attackDuration);
            if (attackProgression == attackDuration)
            {
                isAttacking = false;
                attackRegeneration = attackCooldown;
                attackProgression = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If sword hits an enemy destroy it
        if(collision.GetComponent<EnemyController>())
        {
            Destroy(collision.gameObject);
        }
    }

    public void Attack(Direction direction)
    {
        if(!isAttacking && attackRegeneration == 0f)
        {
            anim.SetTrigger(direction.ToString());
            isAttacking = true;
        }
    }
}
