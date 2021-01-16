using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{

    protected PlayerMovement playerMovement;
    protected bool isFadingOut;
    private HUDController hud;

    public float fadeTime;
    public string sceneToLoad;
    public string startPointName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerMovement.FreezePlayer();
            FadeOut();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        hud = FindObjectOfType<HUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingOut)
        {
            hud.FadeAlpha += Time.deltaTime / fadeTime;

            if (hud.FadeAlpha >= 1f)
            {
                isFadingOut = false;
                LoadScene();
            }
        }
    }

    public void FadeOut()
    {
        isFadingOut = true;
        hud.FadeAlpha = 0;
    }

    public void LoadScene()
    {
        GameManager.instance.startPointName = startPointName;
        print(GameManager.instance.startPointName);
        SceneManager.LoadScene(sceneToLoad);
    }
}
