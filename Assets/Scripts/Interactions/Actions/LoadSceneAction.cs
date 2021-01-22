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

        protected void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _hud = FindObjectOfType<HUDManager>();
        }

        protected override void OnEnterNode()
        {
            _playerMovement.FreezePlayer();
            FadeOut();
        }

        void Update()
        {
            if (_isFadingOut)
            {
                _hud.FadeAlpha += Time.deltaTime / FadeTime;

                if (_hud.FadeAlpha >= 1f)
                {
                    _isFadingOut = false;
                    LoadScene();
                }
            }
        }

        private void FadeOut()
        {
            _isFadingOut = true;
            _hud.FadeAlpha = 0;
        }

        private void LoadScene()
        {
            var saveManager = FindObjectOfType<SceneSaveManager>();
            if (saveManager != null)
            {
                saveManager.Persist();
            }
            GameManager.instance.startPointName = StartPointName;
            print(GameManager.instance.startPointName);
            SceneManager.LoadScene(SceneName);
        }
    }
}