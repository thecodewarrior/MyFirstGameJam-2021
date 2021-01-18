using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NewSaveDialogUIController : AbstractUIController
{
    protected override string TemplateName => "new_save_dialog";
    
    public string GameSceneName;
    private SceneSaveManager _saveManager;

    protected override void Bind()
    {
        Root.Q<Button>("cancel").clicked += CancelClicked;
        Root.Q<Button>("create").clicked += CreateClicked;
    }

    private void CancelClicked()
    {
        Manager.OpenDialog(null);
    }

    private void CreateClicked()
    {
        var saveName = Root.Q<TextField>("file_name").value;
        if (saveName != "")
        {
            GlobalSaveManager.CurrentSaveName = saveName;
            SceneManager.LoadScene(GameSceneName);
        }
    }
}