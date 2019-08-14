using UnityEngine;

// Enemy controlller is the base class for every enemy
public class EnemyController : MonoBehaviour
{
    [SerializeField] protected Gun gun;
    [SerializeField] protected float moveSpeed = 2f;
}
