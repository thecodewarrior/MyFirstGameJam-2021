using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    protected bool isFadingIn;
    public StartPoint currentStartPoint;
    protected PlayerMovement playerMovement;
    private HUDController hud;

    public string levelMusic;
    public bool playLevelMusicOnStart;
    

    public float fadeTime;
    public StartPoint[] startPoints;

    void Start()
    {
        hud = FindObjectOfType<HUDController>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        FindStartPoint();
        PlacePlayerAtStart();

        if (playLevelMusicOnStart)
        {
            if(SoundManager.instance.currentMusicPlaying != levelMusic)
            {
                SoundManager.instance.FadeOutAll(0.1f);
                Invoke("PlayLevelMusic", 0.5f);
            }
        }

        FadeIn();
    }

    void Update()
    {
        if (isFadingIn)
        {
            hud.FadeAlpha -= Time.deltaTime / fadeTime;

            if (hud.FadeAlpha <= 0f)
            {
                isFadingIn = false;
            }
        }
    }

    public void PlayLevelMusic()
    {
        SoundManager.instance.PlayMusic(levelMusic);
    }

    public void FadeIn()
    {
        isFadingIn = true;
        hud.FadeAlpha = 1f;
    }

    public void FindStartPoint()
    {
        if(GameManager.instance.startPointName != null)
        {
            for (int i = 0; i < startPoints.Length; i++)
            {
                if (GameManager.instance.startPointName == startPoints[i].startPointName)
                {
                    currentStartPoint = startPoints[i];
                }
            }
        }

        if (currentStartPoint == null)
        {
            currentStartPoint = startPoints[0];
        }
    }

    public void PlacePlayerAtStart()
    {
        playerMovement.transform.position = currentStartPoint.gameObject.transform.position;
    }

    public void ReduceHealth()
    {

    }

    public void KillPlayer()
    {

    }

}
