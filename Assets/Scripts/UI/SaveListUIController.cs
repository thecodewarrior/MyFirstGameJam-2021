using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SaveListUIController : AbstractUIController
{
    private ListView _saveListView;

    public VisualTreeAsset ElementTemplate;
    public string GameSceneName;

    private List<string> _saveNames;

    protected override void Bind()
    {
        LoadSaveNames();
        _saveListView = Root.Q<ListView>("save_list");
        _saveListView.itemHeight = 26;

        _saveListView.makeItem = () =>
        {
            var element = ElementTemplate.Instantiate();
            element.userData = 0;
            element.Q<Button>("load_button").clicked += () => { OpenSave((int) element.userData); };
            element.Q<Button>("delete_button").clicked += () => { DeleteSave((int) element.userData); };
            return element;
        };
        _saveListView.bindItem = (element, i) =>
        {
            var saveName = _saveNames[i];
            element.userData = i;
            element.Q<Label>("save_name").text = saveName;
        };
        _saveListView.itemsSource = _saveNames;

        Root.Q<Button>("back_button").clicked += BackClicked;
    }

    protected override void Unbind()
    {
        _saveListView = null;
    }

    private void BackClicked()
    {
        Manager.Pop();
    }

    private void OpenSave(int index)
    {
        GlobalSaveManager.CurrentSaveName = _saveNames[index];
        SceneManager.LoadScene(GameSceneName);
    }

    private void DeleteSave(int index)
    {
        if (Active)
        {
            GlobalSaveManager.CurrentSaveName = _saveNames[index];
            var savePath = GlobalSaveManager.CurrentSaveFilePath();
            if (savePath != null)
                File.Delete(savePath);
            GlobalSaveManager.CurrentSaveName = null;
            _saveNames.RemoveAt(index);
            _saveListView.Refresh();
        }
    }

    private void LoadSaveNames()
    {
        GlobalSaveManager.CreateSaveDirectory();
        _saveNames.Clear();
        _saveNames.AddRange(Directory.GetFiles(GlobalSaveManager.SaveDirectory, "*.xml")
            .Select(Path.GetFileName)
            .Select((fileName) => fileName.Substring(0, fileName.Length - 4))
        );
    }
}