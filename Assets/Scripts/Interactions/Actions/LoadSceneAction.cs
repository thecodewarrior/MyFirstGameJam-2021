using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Load Scene")]
    public class LoadSceneAction : AbstractAction
    {
        public string SceneName;
        public string StartPointName;
        public float FadeTime;

        private PlayerMovement _playerMovement;
        private bool _isFadingOut;
        private HUDManager _hud;
        private Health _playerHealth;

        protected void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _hud = FindObjectOfType<HUDManager>();
            _playerHealth = FindObjectOfType<Health>();   
        }

        protected override void OnEnterNode()
        {
            _playerMovement.FreezePlayer();
            if(_playerHealth != null)
                _playerHealth.isInvunerable = true;
            Invoke("LoadScene", FadeTime);
            FadeOut();
        }

        void Update()
        {
            if (_isFadingOut)
            {
                _hud.FadeAlpha += Time.deltaTime / FadeTime;
            }
        }

        private void FadeOut()
        {
            _isFadingOut = true;
            _hud.FadeAlpha = 0;
        }

        public void LoadScene()
        {
            GlobalPlayerData.SceneName = SceneName;
            GlobalPlayerData.SceneEntrance = StartPointName;
            
            if(_playerHealth != null) 
                _playerHealth.isInvunerable = false;
            var saveManager = FindObjectOfType<SceneSaveManager>();
            if (saveManager != null)
            {
                saveManager.Save();
            }
            SceneManager.LoadScene(GlobalPlayerData.SceneName);
        }
    }
}