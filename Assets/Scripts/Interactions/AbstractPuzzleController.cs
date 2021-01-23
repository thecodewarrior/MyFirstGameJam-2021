using UnityEngine;

namespace Interactions
{
    public abstract class AbstractPuzzleController : MonoBehaviour
    {
        private string originalMusic;
        public Canvas Canvas;
        
        public delegate void CompletionCallback(Result result);
        public CompletionCallback OnCompletion;

        public void Show()
        {
            Canvas.gameObject.SetActive(true);
            originalMusic = SoundManager.instance.currentMusicPlaying;
            SoundManager.instance.FadeOutSound(SoundManager.instance.currentMusicPlaying, 1f);
            Invoke("PlayPuzzleMusic", 1.1f);
        }
        
        public void Hide()
        {
            Canvas.gameObject.SetActive(false);
            SoundManager.instance.FadeOutSound(SoundManager.instance.currentMusicPlaying, 1f);
            Invoke("PlayOriginalMusic", 1.1f);
        }

        public void PlayPuzzleMusic()
        {
            SoundManager.instance.PlayMusic("puzzle_music");
        }

        public void PlayOriginalMusic()
        {
            SoundManager.instance.PlayMusic(originalMusic);
        }

        public abstract void ResetPuzzle();
        
        public enum Result
        {
            Success, Fail, Quit
        }
    }
}