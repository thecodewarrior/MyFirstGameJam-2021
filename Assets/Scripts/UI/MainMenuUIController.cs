using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUIController : AbstractUIController
{
    public AbstractUIController SaveListController;
    public AbstractUIController NewSaveDialogController;

    protected override void Bind()
    {
        Root.Q<Button>("new_game").clicked += NewGameClicked;
        Root.Q<Button>("load_game").clicked += LoadGameClicked;
        Root.Q<Button>("quit").clicked += QuitClicked;
    }

    private void NewGameClicked()
    {
        Manager.OpenDialog(NewSaveDialogController);
    }

    private void LoadGameClicked()
    {
        Manager.Push(SaveListController);
    }

    private void QuitClicked()
    {
        Application.Quit();
    }
}