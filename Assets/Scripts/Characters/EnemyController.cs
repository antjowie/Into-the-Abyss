using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] protected Gun gun;
    [SerializeField] protected float moveSpeed = 2f;

    [SerializeField, Tooltip("The state which this enemy starts on")]
    private EnemyStateType startingStateType;

    protected EnemyState currentEnemyState;

    protected abstract void OnStart();

    private void Start() {
        OnStart();
        SwitchToState(startingStateType);
    }

    protected abstract void OnUpdate();

    private void Update()
    {
        OnUpdate();
        currentEnemyState?.UpdateState(Time.deltaTime);
    }

    protected abstract void OnFixedUpdate();

    private void FixedUpdate()
    {
        OnUpdate();
        currentEnemyState?.FixedUpdateState(Time.fixedDeltaTime);
    }

    // We let the inheriting class determine how it wants to switch to a state.
    // we will only handle exiting and entering of the states.
    protected abstract void HandleSwitchingState(EnemyStateType stateToSwitchTo);

    public void SwitchToState(EnemyStateType newStateType)
    {
        currentEnemyState?.ExitState();

        HandleSwitchingState(newStateType);

        currentEnemyState.EnterState();
    }
}
