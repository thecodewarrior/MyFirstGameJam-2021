using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Play Music")]
    public class PlayMusicAction : AbstractAction
    {
        public string MusicName;
        public float FadeTime = 0.1f;
        public bool AdvanceImmediately = true;
        
        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            if (AdvanceImmediately)
            {
                SoundManager.instance.FadeAndPlayMusic(MusicName, FadeTime);
                AdvanceTo(Next);
            }
            else
            {
                SoundManager.instance.FadeAndPlayMusic(MusicName, FadeTime, () => AdvanceTo(Next));
            }
        }
    }
}