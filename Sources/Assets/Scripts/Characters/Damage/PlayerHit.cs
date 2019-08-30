using UnityEngine;

class PlayerHit : Hitable
{
    private PlayerController player = null;

    public override void OnHit(Bullet bullet)
    {
        // TODO, add a fsm to the player so that it can easily define on enter state changes
        player.state = PlayerController.PlayerState.Dead;
        Destroy(bullet);
    }

    void Start()
    {
        player = GetComponent<PlayerController>();   
    }
}