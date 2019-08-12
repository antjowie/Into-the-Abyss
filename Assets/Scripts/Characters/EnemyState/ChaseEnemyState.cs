using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemyState : EnemyState
{
    public override EnemyStateType StateType
    {
        get { return EnemyStateType.Chase; }
    }

    private Transform targetToChase;

    private float catchupDistance;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemyController"></param>
    /// <param name="_targetToChase">The target to chase</param>
    /// <param name="_catchupDistance">How far must this enemy be away from the target to considered as "catched up" to the target.</param>
    public ChaseEnemyState(EnemyController enemyController, Transform _targetToChase, float _catchupDistance) : base(enemyController)
    {
        targetToChase = _targetToChase;
        catchupDistance = _catchupDistance;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {

    }

    public override void UpdateState(float deltaTime)
    {
        if (EnemyNeedsToChase())
        {
            MoveEnemyToTarget();
        }
        else
        {
            // Enemy has already caught up with the desired target, switch back to idle.
            // TODO: Or some other state.
            ControllingEnemy.SwitchToState(EnemyStateType.Idle);
        }
    }

    #region Util

    private void MoveEnemyToTarget()
    {
        if (ControllingEnemy is IMovableEnemy)
        {
            Vector2 directionToMoveTo = targetToChase.position - ControllingEnemy.transform.position;

            (ControllingEnemy as IMovableEnemy).ChangeMoveDirection(directionToMoveTo);
        }
        else
        {
            // TODO: Enemy cannot move but is in a chase state. (Log error or something?)
        }
    }

    private bool EnemyNeedsToChase()
    {
        Vector2 distanceToTarget = ControllingEnemy.transform.position - targetToChase.position;

        return distanceToTarget.magnitude > catchupDistance;
    }

    #endregion
}
