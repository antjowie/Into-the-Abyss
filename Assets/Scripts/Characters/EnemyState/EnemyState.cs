
public abstract class EnemyState
{
    public abstract EnemyStateType StateType { get; }

    /// <summary>
    /// The enemy this state is currently controlling
    /// </summary>
    protected EnemyController ControllingEnemy { get; private set; }

    public EnemyState(EnemyController enemyController) {
        ControllingEnemy = enemyController;
    }

    public abstract void EnterState();
    public abstract void UpdateState(float deltaTime);
    public abstract void FixedUpdateState(float fixedDeltaTime);
    public abstract void ExitState();
}
