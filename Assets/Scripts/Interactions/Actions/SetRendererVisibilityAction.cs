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
    }
}