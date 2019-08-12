using System;
using UnityEngine;

public class IdleEnemyState : EnemyState
{
    public override EnemyStateType StateType {
        get {
            return EnemyStateType.Idle;
        }
    }

    private Action idleAction;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemyController"></param>
    /// <param name="_idleAction">What action to take per-update while idling</param>
    public IdleEnemyState(EnemyController enemyController, Action _idleAction = null) : base(enemyController) {
        idleAction = _idleAction;
    }

    public override void EnterState()
    {
        if (ControllingEnemy is IMovableEnemy) {
            (ControllingEnemy as IMovableEnemy).ChangeMoveDirection(Vector2.zero);
        }
    }

    public override void ExitState()
    {
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
    }

    public override void UpdateState(float deltaTime)
    {
        idleAction?.Invoke();
    }
}
