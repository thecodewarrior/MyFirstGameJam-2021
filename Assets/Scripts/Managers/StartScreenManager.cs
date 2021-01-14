using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{

    public GameObject startMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
