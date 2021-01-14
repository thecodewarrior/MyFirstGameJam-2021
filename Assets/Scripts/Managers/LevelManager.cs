using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public string levelMusic;
    public bool playLevelMusicOnStart;
    // Start is called before the first frame update
    void Start()
    {
        if (playLevelMusicOnStart)
        {
            SoundManager.instance.FadeOutAll(0.1f);
            Invoke("PlayLevelMusic", 0.5f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevelMusic()
    {
        SoundManager.instance.Play(levelMusic);
    }
}
