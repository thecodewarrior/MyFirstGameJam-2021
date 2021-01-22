using System;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Puzzle")]
    public class PuzzleAction : AbstractAction
    {
        public AbstractPuzzleController Controller;
        
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
            Controller.Show();
            Controller.ResetPuzzle();
        }

        protected override void OnExitNode()
        {
            _playerMovement.UnFreezePlayer();
            _hudManager.Show();
        }

        private void OnCompletion(bool wasSuccessful)
        {
            Controller.Hide();
            AdvanceTo(wasSuccessful ? Success : Fail);
        }
    }
}