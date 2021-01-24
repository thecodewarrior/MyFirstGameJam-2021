using System;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Set Player Facing")]
    public class SetPlayerFacing : AbstractAction
    {
        [SerializeField] private FacingDirection Direction;

        public InteractionNode Next;

        private PlayerMovement _playerMovement;

        protected override void InitializeNode()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
        }

        protected override void OnEnterNode()
        {
            _playerMovement.Facing = Direction;
            
            AdvanceTo(Next);
        }
    }
}