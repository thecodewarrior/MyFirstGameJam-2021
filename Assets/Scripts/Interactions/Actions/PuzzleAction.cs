using System;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Puzzle")]
    public class PuzzleAction : AbstractAction
    {
        public AbstractPuzzleController Controller;
        
        public InteractionNode Quit;
        public InteractionNode Fail;
        public InteractionNode Success;

        private PlayerMovement _playerMovement;
        private HUDManager _hudManager;

        private void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _hudManager = FindObjectOfType<HUDManager>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Controller.OnCompletion += OnCompletion;
        }

        protected virtual void OnDisable()
        {
            Controller.OnCompletion -= OnCompletion;
        }

        protected override void OnEnterNode()
        {
            _playerMovement.FreezePlayer();
            _hudManager.Hide();
            UIManager.UnityUIHasInputFocus = true;
            Controller.Show();
            Controller.ResetPuzzle();
        }

        protected override void OnExitNode()
        {
            _playerMovement.UnFreezePlayer();
            _hudManager.Show();
            UIManager.UnityUIHasInputFocus = false;
        }

        private void OnCompletion(AbstractPuzzleController.Result result)
        {
            Controller.Hide();
            switch (result)
            {
                case AbstractPuzzleController.Result.Success:
                    AdvanceTo(Success);
                    break;
                case AbstractPuzzleController.Result.Fail:
                    AdvanceTo(Fail);
                    break;
                case AbstractPuzzleController.Result.Quit:
                    AdvanceTo(Quit);
                    break;
                default:
                    AdvanceTo(Quit);
                    break;
            }
        }
    }
}