using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    private bool isShowingCredits;
    private bool canInterAct;

    public GameObject mainMenuScreen;
    public GameObject creditScreen;

    public Button newGameButton;
    public Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        GlobalSaveManager.CurrentSaveName = "main";
        SoundManager.instance.FadeOutSound(SoundManager.instance.currentMusicPlaying, 1f);
        Invoke("PlayMusic", 1.1f);
        ShowMainMenu();
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
}
