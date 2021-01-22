using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuUIController : AbstractUIController
{
    protected override string TemplateName => "pause_menu";
    
    public string MainMenuName;
    private SceneSaveManager _saveManager;

    protected override void Start()
    {
        base.Start();
        _saveManager = FindObjectOfType<SceneSaveManager>();
    }

    protected override void Bind()
    {
        Root.Q<Button>("resume").clicked += ResumeClicked;
        Root.Q<Button>("main_menu").clicked += MainMenuClicked;
    }

    protected override void Unbind()
    {
        base.Unbind();
    }

    private void ResumeClicked()
    {
        Manager.Pop();
    }

    private void MainMenuClicked()
    {
        SceneManager.LoadScene(MainMenuName);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (Active)
            {
                Manager.Pop();
            }
            else if (!UIManager.HasInputFocus)
            {
                Manager.Push(this);
            }
        }
    }
}