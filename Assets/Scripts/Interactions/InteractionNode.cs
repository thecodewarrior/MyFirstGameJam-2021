using System;
using UnityEngine;

namespace Interactions
{
    public abstract class InteractionNode : MonoBehaviour
    {
        [Tooltip("Whether this is the active node. Purely for readout in the inspector")] [SerializeField]
        private bool _isCurrent;

        /**
         * Whether this is the current node. If this is false, the node should do nothing.
         */
        public bool IsCurrent
        {
            get => _isCurrent;
            private set => _isCurrent = value;
        }

        [NonSerialized] public InteractionManager Manager;

        /**
         * Whether entering this node should be delayed until LateUpdate. This is used to prevent multiple triggers
         * occurring on a single frame.
         */
        protected virtual bool EnterLate => false;

        private bool _delayedEnter;

        /**
         * Called during the first OnEnable to initially set up the node
         */
        protected virtual void InitializeNode()
        {
        }

        /**
         * Called after this node becomes current
         */
        protected virtual void OnEnterNode()
        {
        }

        /**
         * Called before this node becomes no longer current
         */
        protected virtual void OnExitNode()
        {
        }

        /**
         * The setup functions are Awake -> OnEnable -> Start
         * I can't reliably use other components in Awake, but the current node is enabled in Start
         * So I have poor-man's single-run OnEnable
         */
        private bool _hasEnabled;

        protected virtual void OnEnable()
        {
            if (!_hasEnabled)
            {
                foreach (var collider in GetComponents<Collider2D>())
                {
                    collider.enabled = false;
                }

                InitializeNode();
            }

            _hasEnabled = true;
        }

        protected virtual void Start()
        {
            _isCurrent = false;
        }

        protected virtual void LateUpdate()
        {
            if (_delayedEnter)
            {
                EnterNode();
                _delayedEnter = false;
            }
        }

        public void ExitNode()
        {
            if (!IsCurrent)
                return;
            OnExitNode();
            IsCurrent = false;
            Manager.CurrentNode = null;
        }

        public void EnterNode()
        {
            if (IsCurrent)
                return;
            IsCurrent = true;
            Manager.CurrentNode = this;
            OnEnterNode();
        }

        public void AdvanceTo(InteractionNode next)
        {
            if (!IsCurrent)
            {
                throw new Exception("Tried to advance from a node that isn't current");
            }

            ExitNode();

            if (next == null)
            {
                return;
            }

            if (next.EnterLate)
            {
                next._delayedEnter = true;
            }
            else
            {
                next.EnterNode();
            }
        }
    }
}