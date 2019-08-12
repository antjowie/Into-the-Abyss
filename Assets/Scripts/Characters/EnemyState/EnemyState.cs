
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

    public abstract void OnStateEnter();
    public abstract void OnUpdate(float deltaTime);
    public abstract void OnFixedUpdate(float fixedDeltaTime);
    public abstract void OnStateExit();
}
