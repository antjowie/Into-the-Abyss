using UnityEngine;

// GunStats is responsible for what is fired. It defines what the gun shoots
[CreateAssetMenu(fileName = "GunStatsName",menuName = "Gun Stats",order = 51)]
public class GunStats : ScriptableObject
{
    public Sprite sprite;
    public float fireRate;
    public float bulletSpeed;
    public Bullet bullet;
}
