using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Interactions
{
    public class InteractionSaveManager : MonoBehaviour, IPersistentObject
    {
        public InteractionNode Next;

        private StartNode _startNode;
        private Dictionary<string, SaveNode> _saveNodes = new Dictionary<string, SaveNode>();

        private void Start()
        {
            InitialSaveState = CreateSaveState("");
            foreach (var node in gameObject.GetComponentsInChildren<InteractionNode>())
            {
                node.SaveManager = this;
                switch (node)
                {
                    case StartNode startNode:
                        _startNode = startNode;
                        break;
                    case SaveNode saveNode:
                        _saveNodes[saveNode.ID] = saveNode;
                        break;
                }
            }
        }
        
        [Header("Save Tracked Objects")] [SerializeField]
        private List<GameObject> _activeObjects;

        [SerializeField] private List<Behaviour> _enabledBehaviors;
        [SerializeField] private List<Renderer> _visibleRenderers;
        
        [Header("Serialization")] [SerializeField]
        private string _saveId;

        public string SaveID => _saveId;

        private SaveState InitialSaveState;
        private SaveState LatestSaveState;
        [NonSerialized] public InteractionNode CurrentNode;

        public void MarkSavePoint(SaveNode saveNode)
        {
            LatestSaveState = CreateSaveState(saveNode.ID);
        }

        public SaveState CreateSaveState(string savePoint)
        {
            return new SaveState
            {
                LatestSavePoint = savePoint,
                ActiveObjects = _activeObjects.Select(e => e.activeSelf).ToList(),
                EnabledBehaviors = _enabledBehaviors.Select(e => e.enabled).ToList(),
                VisibleRenderers = _visibleRenderers.Select(e => e.enabled).ToList(),
            };
        }

        public void LoadSaveState(AbstractSaveState saveState)
        {
            var state = saveState as SaveState;
            if (state == null)
                state = InitialSaveState;
            
            if(CurrentNode != null)
                CurrentNode.ExitNode();
            
            LatestSaveState = state.Copy();

            for (var i = 0; i < Math.Min(_activeObjects.Count, state.ActiveObjects.Count); i++)
            {
                _activeObjects[i].SetActive(state.ActiveObjects[i]);
            }

            for (var i = 0; i < Math.Min(_enabledBehaviors.Count, state.EnabledBehaviors.Count); i++)
            {
                _enabledBehaviors[i].enabled = state.EnabledBehaviors[i];
            }

            for (var i = 0; i < Math.Min(_visibleRenderers.Count, state.VisibleRenderers.Count); i++)
            {
                _visibleRenderers[i].enabled = state.VisibleRenderers[i];
            }

            if (_saveNodes.ContainsKey(state.LatestSavePoint))
            {
                _saveNodes[state.LatestSavePoint].Next.EnterNode();
            }
            else
            {
                _startNode.EnterNode();
            }
        }

        public AbstractSaveState GetSaveState()
        {
            return LatestSaveState.Copy();
        }

        [XmlType("StartNode")]
        public class SaveState : AbstractSaveState
        {
            public string LatestSavePoint;
            public List<bool> ActiveObjects;
            public List<bool> EnabledBehaviors;
            public List<bool> VisibleRenderers;

            public SaveState Copy()
            {
                return new SaveState
                {
                    LatestSavePoint = LatestSavePoint,
                    ActiveObjects = ActiveObjects,
                    EnabledBehaviors = EnabledBehaviors,
                    VisibleRenderers = VisibleRenderers,
                };
            }
        }
    }
}