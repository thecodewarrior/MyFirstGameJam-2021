using UnityEngine;

namespace Interactions
{
    [AddComponentMenu("Interaction/Save Point")]
    public class SaveNode : InteractionNode
    {
        public string ID;
        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            if(Manager != null)
                Manager.MarkSavePoint(this);
            AdvanceTo(Next);
        }
    }
}