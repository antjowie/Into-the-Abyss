using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] protected Gun gun;
    [SerializeField] protected float moveSpeed = 2f;
}
