using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    public Canvas DeathTitle;
    public float RespawnTime;

    public void ShowDeath()
    {
        DeathTitle.gameObject.SetActive(true);
        Invoke(nameof(ReloadScene), RespawnTime);
    }

    public void ReloadScene()
    {
        GlobalPlayerData.Load();
        GlobalPlayerData.ResetHealth();
        SceneManager.LoadScene(gameObject.scene.name);
    }
}