class EnemyHit: Hitable
{
    public override void OnHit(Bullet bullet)
    {
        if(bullet.fromObject.name == "Player")
        {
            Destroy(gameObject);
            Destroy(bullet.gameObject);
        }
    }
}