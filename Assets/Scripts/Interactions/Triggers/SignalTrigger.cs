using UnityEngine;

namespace Interactions.Triggers
{
    [AddComponentMenu("Interaction/Trigger/Signal Trigger")]
    public class SignalTrigger : AbstractTrigger
    {
        public InteractionNode Next;

        public void Fire()
        {
            AdvanceTo(Next);
        }
    }
}