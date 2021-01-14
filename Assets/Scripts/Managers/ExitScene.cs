using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{

    protected PlayerMovement playerMovement;
    protected bool isFadingOut;
    protected Image fadeImage;
    protected FadeObject fadeObject;

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
        fadeObject = FindObjectOfType<FadeObject>();
        fadeImage = fadeObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingOut)
        {
            Color objectColor = fadeImage.color;
            float fadeAmount = objectColor.a + (Time.deltaTime / fadeTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeImage.color = objectColor;

            if (objectColor.a >= 1f)
            {
                isFadingOut = false;
                LoadScene();
            }
        }
    }

    public void FadeOut()
    {
        isFadingOut = true;
    }

    public void LoadScene()
    {
        GameManager.instance.startPointName = startPointName;
        print(GameManager.instance.startPointName);
        SceneManager.LoadScene(sceneToLoad);
    }
}
