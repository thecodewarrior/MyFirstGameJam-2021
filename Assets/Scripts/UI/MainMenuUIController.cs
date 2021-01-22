using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUIController : AbstractUIController
{
    protected override string TemplateName => "main_menu";

    public string GameSceneName;

    protected override void Bind()
    {
        GlobalSaveManager.CurrentSaveName = "main";
        
        Root.Q<Button>("new_game").clicked += NewGameClicked;
        var continueButton = Root.Q<Button>("continue");
        continueButton.clicked += ContinueClicked;
        Root.Q<Button>("quit").clicked += QuitClicked;

        continueButton.style.display = new StyleEnum<DisplayStyle>(
            File.Exists(GlobalSaveManager.CurrentSaveFilePath()) ? DisplayStyle.Flex : DisplayStyle.None
        );
    }

    private void NewGameClicked()
    {
        File.Delete(GlobalSaveManager.CurrentSaveFilePath());
        GlobalSaveManager.Reset();
        GlobalPlayerData.Load();
        SceneManager.LoadScene(GameSceneName);
    }

    private void ContinueClicked()
    {
        GlobalSaveManager.ReadFromFile();
        GlobalPlayerData.Load();
        SceneManager.LoadScene(GlobalPlayerData.SceneName);
    }

    private void QuitClicked()
    {
        Application.Quit();
    }
}