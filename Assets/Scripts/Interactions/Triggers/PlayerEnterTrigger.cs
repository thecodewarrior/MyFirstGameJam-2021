using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Triggers
{
    [AddComponentMenu("Interaction/Trigger/Player Enter Trigger")]
    public class PlayerEnterTrigger : AbstractTrigger
    {
        [Tooltip("The colliders to use for the trigger. These will be enabled/disabled as necessary")]
        public List<Collider2D> TriggerColliders;

        [Tooltip("If present, the player will be made to face this object when they hit the trigger")]
        public GameObject FacePlayerToward;

        [Tooltip("If present, the this object will be made to face the player when they hit the trigger")]
        public GameObject FaceTowardPlayer;

        [Tooltip("The node to advance to when the player hits the trigger")]
        public InteractionNode Next;

        private PlayerMovement _playerMovement;

        protected override void InitializeNode()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
        }

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
            if (IsCurrent && collision.CompareTag("Player"))
            {
                MakePlayerFaceTarget();
                MakeTargetFacePlayer();
                AdvanceTo(Next);
            }
        }

        private void MakePlayerFaceTarget()
        {
            var target = FacePlayerToward;
            if (!target)
                return;

            if (target.transform.position.x > _playerMovement.gameObject.transform.position.x)
            {
                _playerMovement.MakePlayerFaceRight();
            }
            else
            {
                _playerMovement.MakePlayerFaceLeft();
            }
        }

        private void MakeTargetFacePlayer()
        {
            var target = FaceTowardPlayer;
            if (!target)
                return;

            if (target.transform.position.x > _playerMovement.gameObject.transform.position.x)
            {
                var localScale = target.transform.localScale;
                localScale.x = Mathf.Abs(localScale.x);
                target.transform.localScale = localScale;
            }
            else
            {
                var localScale = target.transform.localScale;
                localScale.x = -Mathf.Abs(localScale.x);
                target.transform.localScale = localScale;
            }
        }
    }
}