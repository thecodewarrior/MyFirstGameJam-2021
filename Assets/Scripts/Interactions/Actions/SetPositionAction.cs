using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Set Position")]
    public class SetPositionAction : AbstractAction
    {
        public Transform ObjectToMove;
        public Transform TargetPosition;

        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            ObjectToMove.position = TargetPosition.position;
            
            AdvanceTo(Next);
        }
    }
}