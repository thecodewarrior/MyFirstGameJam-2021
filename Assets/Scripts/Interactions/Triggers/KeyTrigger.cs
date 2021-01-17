using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Interactions.Triggers
{
    [AddComponentMenu("Interaction/Trigger/Key Trigger")]
    public class KeyTrigger : AbstractTrigger
    {
        public AbstractHUDController Prompt;
        
        [Tooltip("When the player is inside this collider, pressing the 'interact' key will advance to the next node")]
        public Collider2D TriggerCollider;

        [Tooltip("The node to advance to when the player walks up to the collider and presses the 'interact' key")]
        public InteractionNode Next;

        private bool _playerIsInside;
        private HUDManager _hudManager;

        protected override void InitializeNode()
        {
            _hudManager = FindObjectOfType<HUDManager>();
        }

        public void Update()
        {
            if (IsCurrent && _playerIsInside && Input.GetButtonDown("Interact"))
            {
                if(Prompt)
                    _hudManager.HideController(Prompt);
                AdvanceTo(Next);
            }
        }

        protected override void OnEnterNode()
        {
            TriggerCollider.enabled = true;
        }

        protected override void OnExitNode()
        {
            TriggerCollider.enabled = false;
            _playerIsInside = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerIsInside = true;
                if(Prompt)
                    _hudManager.ShowController(Prompt);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerIsInside = false;
                if(Prompt)
                    _hudManager.HideController(Prompt);
            }
        }
    }
}