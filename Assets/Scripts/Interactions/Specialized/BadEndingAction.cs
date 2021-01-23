using System.Collections.Generic;
using Interactions.Actions;
using UnityEngine.SceneManagement;

namespace Interactions.Specialized
{
    public class BadEndingAction : AbstractAction
    {
        public float RespawnTime;

        public List<InventoryItem> RemoveItems;
        public List<InventoryItem> AddItems;

        public LoadSceneAction RespawnAction;
        
        private DeathController _deathController;

        protected override void InitializeNode()
        {
            _deathController = FindObjectOfType<DeathController>();
        }

        protected override void OnEnterNode()
        {
            _deathController.ShowDeath(false);
            Invoke(nameof(Respawn), RespawnTime);
        }

        public void Respawn()
        {
            foreach (var item in RemoveItems)
            {
                GlobalPlayerData.Inventory.RemoveItem(item);
            }
            foreach (var item in AddItems)
            {
                GlobalPlayerData.Inventory.InsertItem(item, 1);
            }
            GlobalPlayerData.ResetHealth();
            
            UIManager.UnityUIHasInputFocus = false;
            AdvanceTo(RespawnAction);
        }
    }
}