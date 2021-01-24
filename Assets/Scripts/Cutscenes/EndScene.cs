using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{

    private bool isFadingIn;
    private bool isFadingOut;
    private bool canInteract;

    public string musicName;
    public float timeBeforeStoryActivation;
    public float timeBeforeFadeOut;
    public float fadeTime;
    public GameObject storyObject;
    public Image fadeImage;
    
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.FadeOutSound(SoundManager.instance.currentMusicPlaying, 1f);
        SoundManager.instance.PlayMusic(musicName);
        Invoke("ActivateStoryObject", timeBeforeStoryActivation);
        Invoke("AllowInteraction", 2f);
        Invoke("FadeOut", timeBeforeFadeOut);
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
        storyObject.SetActive(false);
        FadeIn();
    }

    public void ActivateStoryObject()
    {
        storyObject.SetActive(true);
    }

    public void PlayMusic()
    {
        SoundManager.instance.PlayMusic(musicName);
    }

    public void FadeOutMusic()
    {
        SoundManager.instance.FadeOutAll(fadeTime);
    }

    void Update()
    {
        PerformFade();
        //if (canInteract)
        //{
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        LoadMainMenu();
        //    }
        //}
    }

    public void PerformFade()
    {
        if (isFadingIn)
        {
            Color fadeObjectColor = fadeImage.color;
            float fadeAmount = fadeObjectColor.a - (Time.deltaTime/fadeTime);

            fadeObjectColor = new Color(fadeObjectColor.r, fadeObjectColor.g, fadeObjectColor.b, fadeAmount);
            fadeImage.color = fadeObjectColor;
            if (fadeObjectColor.a <= 0f)
            {
                isFadingIn = false;
            }
        }

        if (isFadingOut)
        {
            Color fadeObjectColor = fadeImage.color;
            float fadeAmount = fadeImage.color.a + (Time.deltaTime/fadeTime);
            fadeObjectColor = new Color(fadeObjectColor.r, fadeObjectColor.g, fadeObjectColor.b, fadeAmount);
            fadeImage.color = fadeObjectColor;
            if (fadeObjectColor.a >= 1f)
            {
                isFadingOut = false;
                Invoke("LoadMainMenu", fadeTime);
                FadeOutMusic();
            }
        }
    }

    public void FadeIn()
    {
        isFadingIn = true;
    }

    public void FadeOut()
    {
        isFadingOut = true;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AllowInteraction()
    {
        canInteract = true;
    }
}
