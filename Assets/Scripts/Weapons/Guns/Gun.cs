using UnityEngine;

// The gun class is responsible for being the game object. It is the interface that is used to make a weapon fire.
// It is responsible for displaying the weapon and updating its state using the strategy pattern.
public class Gun : MonoBehaviour
{
    [SerializeField] GunStats gunStats = null;

    Vector2 targetPos;
    float shotCooldown = 0f;

    // This function has to be called after press or released fire is called by the AI
    public void Update()
    {
        shotCooldown = Mathf.Clamp(shotCooldown - Time.deltaTime,0, 1f / gunStats.fireRate);
    }

    public void Aim(Vector2 target)
    {
        // We keep track of the targetPos because the shoot function uses it
        targetPos = target;

        //Debug.DrawRay(transform.position, transform.forward);

        // This variable rotates to forward to point to the right. Because our image points to x = 1 but the actual forward
        // points to z = -1. Because of this, we first rotate the image to face the forward. The lookat solves the rest.
        Quaternion forwardRotation = Quaternion.identity;
        forwardRotation.eulerAngles = new Vector3(0,-90,0);

        // Create a lookat quaternion. This also handles y rotation so the image is flipped.
        Vector3 lookat = (Vector3)targetPos - transform.position;
        Quaternion rotation= Quaternion.LookRotation(lookat);
        
        // First we change the image to face in the forward direction, then we rotate it towards the lookat direction.
        transform.rotation = rotation * forwardRotation;
    }

    public void Shoot()
    {
        if(shotCooldown == 0f)
        {
            Bullet bullet = Instantiate(gunStats.bullet,transform.position,Quaternion.identity);
            bullet.Launch(gameObject, targetPos - (Vector2)transform.position, gunStats.bulletSpeed);
            shotCooldown = 1f / gunStats.fireRate;
        }
    }
    
    private void OnValidate()
    {
        if(gunStats)
        {
            GetComponent<SpriteRenderer>().sprite = gunStats.sprite;
        }
    }
}
