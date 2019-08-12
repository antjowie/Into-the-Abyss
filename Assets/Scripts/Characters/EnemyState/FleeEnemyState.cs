using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeEnemyState : EnemyState
{
    public override EnemyStateType StateType {
        get {
            return EnemyStateType.Flee;
        }
    }

    private float fleeSuccessDistance;

    private Transform targetToFleeFrom;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemyController"></param>
    /// <param name="_targetToFleeFrom">The target to flee from</param>
    /// <param name="_fleeSuccessDistance">How far the enemy has to be away from the target to be considered as successful at fleeing.</param>
    public FleeEnemyState(EnemyController enemyController, Transform _targetToFleeFrom, float _fleeSuccessDistance) : base(enemyController) {
        targetToFleeFrom = _targetToFleeFrom;
        fleeSuccessDistance = _fleeSuccessDistance;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        if (ControllingEnemy is IMovableEnemy)
        {
            (ControllingEnemy as IMovableEnemy).ChangeMoveDirection(Vector2.zero);
        }
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
    }

    public override void UpdateState(float deltaTime)
    {
        if (EnemyNeedsToFlee())
        {
            MoveEnemyAwayFromTarget();
        }
        else {
            // Enemy has already fled from the desired target, switch back to idle.
            // TODO: Or some other state.
            ControllingEnemy.SwitchToState(EnemyStateType.Idle);
        }
    }

    #region Util

    private void MoveEnemyAwayFromTarget() {
        if (ControllingEnemy is IMovableEnemy)
        {
            Vector2 fleeDirection = ControllingEnemy.transform.position - targetToFleeFrom.position;
            (ControllingEnemy as IMovableEnemy).ChangeMoveDirection(fleeDirection);
        }
        else {
            // TODO: Enemy cannot move but is in a flee state. (Log error or something?)
        }
    }

    private bool EnemyNeedsToFlee() {
        Vector2 distanceToTarget = ControllingEnemy.transform.position - targetToFleeFrom.position;

        return distanceToTarget.magnitude < fleeSuccessDistance;
    }

    #endregion
}
