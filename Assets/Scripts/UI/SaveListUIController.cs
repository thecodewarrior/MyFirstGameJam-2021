using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SaveListUIController : AbstractUIController
{
    protected override string TemplateName => "save_list";
    private VisualTreeAsset _elementTemplate;
    
    private ListView _saveListView;

    public string GameSceneName;

    private List<string> _saveNames = new List<string>();

    protected override void Start()
    {
        base.Start();
        _elementTemplate = UITemplates.GetTemplate("save_list_element");
    }

    protected override void Bind()
    {
        LoadSaveNames();
        _saveListView = Root.Q<ListView>("save_list");
        _saveListView.itemHeight = 26;

        _saveListView.makeItem = () =>
        {
            var element = _elementTemplate.Instantiate();
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
        GlobalSaveManager.ReadFromFile();
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
        var files = Directory.GetFiles(GlobalSaveManager.SaveDirectory, "*.xml");
        _saveNames.AddRange(files
            .Select(Path.GetFileName)
            .Select((fileName) => fileName.Substring(0, fileName.Length - 4))
        );
    }
}