using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Set Renderer Visibility")]
    public class SetRendererVisibilityAction : AbstractAction
    {
        public List<Renderer> Show;
        public List<Renderer> Hide;

        public InteractionNode Next;
        
        protected override void OnEnterNode()
        {
            foreach (var component in Show)
            {
                component.enabled = true;
            }
            foreach (var component in Hide)
            {
                component.enabled = false;
            }
            AdvanceTo(Next);
        }
    }
}