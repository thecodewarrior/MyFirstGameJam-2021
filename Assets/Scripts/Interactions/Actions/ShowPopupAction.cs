using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Show Popup")]
    public class ShowPopupAction : AbstractAction
    {
        protected override bool EnterLate => true;
        
        public List<PopupMessage> Messages;

        public InteractionNode Next;
        
        private PopupDialogUIController _popupController;

        protected override void InitializeNode()
        {
            _popupController = FindObjectOfType<PopupDialogUIController>();
        }

        protected override void OnEnterNode()
        {
            _popupController.ShowPopup(this);
        }

        public void EndPopup()
        {
            AdvanceTo(Next);
        }
    }
}