using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    protected bool isFadingIn;
    public StartPoint currentStartPoint;
    protected PlayerMovement playerMovement;
    protected FadeObject fadeObject;
    protected Image fadeImage;

    public string levelMusic;
    public bool playLevelMusicOnStart;
    

    public float fadeTime;
    public StartPoint[] startPoints;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        fadeObject = FindObjectOfType<FadeObject>();
        fadeImage = fadeObject.GetComponent<Image>();

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

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
        FadeIn();
    }

    void Update()
    {
        if (isFadingIn)
        {
            Color objectColor = fadeImage.color;
            float fadeAmount = objectColor.a - (Time.deltaTime / fadeTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeImage.color = objectColor;

            if (objectColor.a <= 0f)
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
}
