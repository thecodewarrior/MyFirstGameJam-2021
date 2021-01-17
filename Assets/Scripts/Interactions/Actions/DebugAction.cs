using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Debug Action")]
    public class DebugAction : AbstractAction
    {
        [Tooltip("The name to show in the debug logs")]
        public string DebugName;

        [Tooltip("The node to immediately advance to")]
        public InteractionNode Next;
        
        protected override void InitializeNode()
        {
            Debug.Log($"Debug action '{DebugName}': InitializeNode called");
        }

        protected override void OnEnterNode()
        {
            Debug.Log($"Debug action '{DebugName}': OnEnterNode called");
            AdvanceTo(Next);
        }

        protected override void OnExitNode()
        {
            Debug.Log($"Debug action '{DebugName}': OnExitNode called");
        }
    }
}