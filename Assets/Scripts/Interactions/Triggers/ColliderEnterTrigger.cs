using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Triggers
{
    [AddComponentMenu("Interaction/Trigger/Collider Enter Trigger")]
    public class ColliderEnterTrigger : AbstractTrigger
    {
        [Tooltip("The colliders to use for the trigger. These will be enabled/disabled as necessary")]
        public List<Collider2D> TriggerColliders;

        [Tooltip("The collider layer to check for")]
        public string Tag;

        [Tooltip("The node to advance to when the player hits the trigger")]
        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            foreach (var trigger in TriggerColliders)
            {
                trigger.enabled = true;
            }
        }

        protected override void OnExitNode()
        {
            foreach (var trigger in TriggerColliders)
            {
                trigger.enabled = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsCurrent && collision.CompareTag(Tag))
            {
                AdvanceTo(Next);
            }
        }

    }
}