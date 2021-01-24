using System.Collections.Generic;
using System.IO;
using Interactions.Actions;
using UnityEngine.SceneManagement;

namespace Interactions.Specialized
{
    public class DeleteSaveAction : AbstractAction
    {
        public InteractionNode Next;

        protected override void InitializeNode()
        {
        }

        protected override void OnEnterNode()
        {
            if (GlobalSaveManager.CurrentSaveFilePath() != null)
            {
                GlobalSaveManager.CreateSaveDirectory();
                File.Delete(GlobalSaveManager.CurrentSaveFilePath());
                GlobalSaveManager.CurrentSaveName = null;
            }

            AdvanceTo(Next);
        }
    }
}