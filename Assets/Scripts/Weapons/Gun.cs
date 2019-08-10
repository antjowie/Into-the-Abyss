using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float BulletSpeed = 10f;

    Vector2 targetPos;
    
    private void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {

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
}
