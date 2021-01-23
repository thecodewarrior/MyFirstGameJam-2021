using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    public Canvas DeathTitle;
    public Canvas DeathTitleNoWolf;
    public float RespawnTime;

    public void ShowDeathAndReload(bool isWolfDeath)
    {
        ShowDeath(isWolfDeath);
        Invoke(nameof(ReloadScene), RespawnTime);
    }

    public void ShowDeath(bool isWolfDeath)
    {
        if (isWolfDeath)
        {
            DeathTitle.gameObject.SetActive(true);  
        } else
        {
            DeathTitleNoWolf.gameObject.SetActive(true);
        }
        UIManager.UnityUIHasInputFocus = true;
    }

    public void ReloadScene()
    {
        GlobalPlayerData.Load();
        GlobalPlayerData.ResetHealth();
        UIManager.UnityUIHasInputFocus = false;
        SceneManager.LoadScene(gameObject.scene.name);
    }
}