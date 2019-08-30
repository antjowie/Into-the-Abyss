using UnityEngine;

public class TimeSinceSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    float initialTime;

    void Start()
    {
        initialTime = Time.timeSinceLevelLoad;
    }
    
    public float ElapsedTime()
    {
        return Time.timeSinceLevelLoad - initialTime;
    }
}
