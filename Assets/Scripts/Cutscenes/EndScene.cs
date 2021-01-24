using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{

    public string musicName;
    public float timeBeforeStoryActivation;
    public float fadeTime;
    public GameObject storyObject;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.FadeOutSound(SoundManager.instance.currentMusicPlaying, 1f);
        SoundManager.instance.PlayMusic(musicName);
        Invoke("ActivateStoryObject", timeBeforeStoryActivation);
    }

    public void ActivateStoryObject()
    {
        storyObject.SetActive(true);
    }

    public void PlayMusic()
    {
        SoundManager.instance.PlayMusic(musicName);
    }
}
