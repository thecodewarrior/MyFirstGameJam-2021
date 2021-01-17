using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Enable & Disable Behaviors")]
    public class SetBehaviorEnabledAction : AbstractAction
    {
        [Tooltip("The list of components to enable")]
        public List<Behaviour> Enable;
        [Tooltip("The list of components to disable")]
        public List<Behaviour> Disable;
        
        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            foreach (var component in Enable)
            {
                component.enabled = true;
            }
            foreach (var component in Disable)
            {
                component.enabled = false;
            }
            AdvanceTo(Next);
        }
    }
}