using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Set Player Frozen")]
    public class SetPlayerFrozenAction : AbstractAction
    {
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
            else
                _playerMovement.UnFreezePlayer();
            AdvanceTo(Next);
        }
    }
}