using UnityEngine;
using UnityEngine.SceneManagement;

class PlayerHit : Hitable
{
    [SerializeField] private float TimeTillRestart = 5f;
    
    public override void OnHit(Bullet bullet)
    {
        gameObject.SetActive(false);
        Destroy(bullet);

        Invoke("RestartScene", TimeTillRestart);
    }
    
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}