using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Set Object Active")]
    public class SetObjectActiveAction : AbstractAction
    {
        [Tooltip("The list of objects to activate")]
        public List<GameObject> Enable;
        [Tooltip("The list of objects to deactivate")]
        public List<GameObject> Disable;
        
        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            foreach (var component in Enable)
            {
                component.SetActive(true);
            }
            foreach (var component in Disable)
            {
                component.SetActive(false);
            }
            AdvanceTo(Next);
        }
    }
}