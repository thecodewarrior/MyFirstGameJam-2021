using Interactions.Triggers;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Send Signal")]
    public class SendSignalAction : AbstractAction
    {
        public SignalTrigger Trigger;
        
        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            if(Trigger != null)
                Trigger.Fire();
            AdvanceTo(Next);
        }
    }
}