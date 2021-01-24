using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    private bool isFadingIn;
    private bool isFadingOut;
    private bool isShowingCredits;
    private bool canInterAct;

    public GameObject mainMenuScreen;
    public GameObject creditScreen;

    public Button newGameButton;
    public Button continueButton;

    public Image fadeImage;
    public float fadeTime;
    // Start is called before the first frame update
    void Start()
    {
        GlobalSaveManager.CurrentSaveName = "main";
        SoundManager.instance.FadeOutSound(SoundManager.instance.currentMusicPlaying, 1f);
        Invoke("PlayMusic", 1.1f);
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
        ShowMainMenu();
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit"))
        {
            if (isShowingCredits && canInterAct)
            {
                ShowMainMenu();
            }
        }
        PerformFade();
    }

    public void NewGameClicked(string GameSceneName)
    {
        GlobalSaveManager.CreateSaveDirectory();
        File.Delete(GlobalSaveManager.CurrentSaveFilePath());
        GlobalSaveManager.Reset();
        GlobalPlayerData.Load();
        SceneManager.LoadScene(GameSceneName);
    }

    public void ContinueClicked()
    {
        GlobalSaveManager.CreateSaveDirectory();
        GlobalSaveManager.ReadFromFile();
        GlobalPlayerData.Load();
        SceneManager.LoadScene(GlobalPlayerData.SceneName);
    }

    public void CheckForSaveFile()
    {
        if (File.Exists(GlobalSaveManager.CurrentSaveFilePath()))
        {
            continueButton.interactable = true;
            FocusOnContinueButton(true);
        } else
        {
            continueButton.interactable = false;
            FocusOnContinueButton(false);
        }
    }

    public void ShowCredits()
    {
        isShowingCredits = true;
        canInterAct = false;
        Invoke("AllowInteraction", 1f);
        mainMenuScreen.SetActive(false);
        creditScreen.SetActive(true);
    }

    public void ShowMainMenu()
    {
        isShowingCredits = false;
        mainMenuScreen.SetActive(true);
        creditScreen.SetActive(false);
        CheckForSaveFile();
    }

    public void FocusOnContinueButton(bool shouldFocusOnContinue)
    {
        if (shouldFocusOnContinue)
        {
            continueButton.Select();
            continueButton.OnSelect(null);
        } else
        {
            newGameButton.Select();
            newGameButton.OnSelect(null);
        } 
    }

    public void AllowInteraction()
    {
        canInterAct = true;
    }

    public void QuitClicked()
    {
        Application.Quit();
    }

    public void PlayMusic()
    {
        SoundManager.instance.PlayMusic("intro_music");
    }

    public void FadeIn()
    {
        isFadingIn = true;
    }

    public void FadeOut()
    {
        isFadingOut = true;
    }

    public void PerformFade()
    {
        if (isFadingIn)
        {
            Color fadeObjectColor = fadeImage.color;
            float fadeAmount = fadeObjectColor.a - (Time.deltaTime / fadeTime);

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
            float fadeAmount = fadeImage.color.a + (Time.deltaTime / fadeTime);
            fadeObjectColor = new Color(fadeObjectColor.r, fadeObjectColor.g, fadeObjectColor.b, fadeAmount);
            fadeImage.color = fadeObjectColor;
            if (fadeObjectColor.a >= 1f)
            {
                isFadingOut = false;
                Invoke("LoadMainMenu", fadeTime);
            }
        }
    }
}
