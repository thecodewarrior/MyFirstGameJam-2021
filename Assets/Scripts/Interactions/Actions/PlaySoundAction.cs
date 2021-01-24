using System.Collections;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Play Sound")]
    public class PlaySoundAction : AbstractAction
    {
        public AudioSource Source;
        public float FadeDelay;
        public float FadeDuration;

        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            Source.Play();

            if (FadeDuration > 0)
            {
                Invoke(nameof(Fade), FadeDelay);
            }
            
            AdvanceTo(Next);
        }

        public void Fade()
        {
            StartCoroutine(FadeOut(FadeDuration));
        }

        public IEnumerator FadeOut(float fadeTime)
        {
            var startVolume = Source.volume;

            while (Source.volume > 0)
            {
                Source.volume -= startVolume * Time.unscaledDeltaTime / fadeTime;
                yield return null;
            }

            Source.Stop();
            Source.volume = startVolume;
        }
    }
}