using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAudio : MonoBehaviour
{

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SoundManager.instance.FadeOutSound(SoundManager.instance.currentMusicPlaying, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
