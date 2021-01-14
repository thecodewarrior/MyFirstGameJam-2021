using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuUIController : AbstractUIController
{
    public string MainMenuName;
    private SceneSaveManager _saveManager;

    private void Start()
    {
        _saveManager = FindObjectOfType<SceneSaveManager>();
    }

    protected override void Bind()
    {
        Root.Q<Button>("resume").clicked += ResumeClicked;
        Root.Q<Button>("save").clicked += SaveClicked;
        Root.Q<Button>("main_menu").clicked += MainMenuClicked;
    }

    private void ResumeClicked()
    {
        Manager.Pop();
    }

    private void MainMenuClicked()
    {
        SceneManager.LoadScene(MainMenuName);
    }

    private void SaveClicked()
    {
        _saveManager.Save();
    }
}