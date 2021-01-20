using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Delay")]
    public class DelayAction : AbstractAction
    {
        public float Delay;
        public bool FreezePlayer;

        public InteractionNode Next;

        private PlayerMovement _playerMovement;

        protected override void InitializeNode()
        {
            base.InitializeNode();
            _playerMovement = FindObjectOfType<PlayerMovement>();
        }

        protected override void OnEnterNode()
        {
            if(FreezePlayer)
                _playerMovement.FreezePlayer();
            StartCoroutine(nameof(TimerEnd));
        }

        public IEnumerator TimerEnd()
        {
            yield return new WaitForSeconds(Delay);
            if (IsCurrent)
            {
                if(FreezePlayer)
                    _playerMovement.UnFreezePlayer();
                AdvanceTo(Next);
            }
        }
    }
}