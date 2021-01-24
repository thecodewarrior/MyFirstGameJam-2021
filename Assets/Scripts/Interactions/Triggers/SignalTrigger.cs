using UnityEngine;

namespace Interactions.Triggers
{
    [AddComponentMenu("Interaction/Trigger/Signal Trigger")]
    public class SignalTrigger : AbstractTrigger
    {
        public InteractionNode Next;

        private bool _hasBeenTriggered;

        protected override void OnEnterNode()
        {
            var shouldImmediatelyTrigger = _hasBeenTriggered;
            _hasBeenTriggered = false;
            if (shouldImmediatelyTrigger)
            {
                AdvanceTo(Next);
            }
        }

        public void Fire()
        {
            if (IsCurrent)
            {
                AdvanceTo(Next);
            }
            else
            {
                _hasBeenTriggered = true;
            }
        }
    }
}