using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAudio : MonoBehaviour
{

    public AudioSource audioSource;
    public bool noDelay;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (noDelay)
        {
            SoundManager.instance.FadeOutSound(SoundManager.instance.currentMusicPlaying, 0.1f);
            Invoke("PlayDeathMusic", 0.2f);
        } else
        {
            SoundManager.instance.FadeOutSound(SoundManager.instance.currentMusicPlaying, 1f);
            Invoke("PlayDeathMusic", 1.1f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void PlayDeathMusic()
    {
        SoundManager.instance.PlayMusic("death_music");
    }
}
