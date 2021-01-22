using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using NUnit.Framework;
using UnityEngine;

namespace Interactions
{
    [AddComponentMenu("Interaction/Interaction Manager")]
    public class InteractionManager : MonoBehaviour, IPersistentObject
    {
        private StartNode _startNode;
        private Dictionary<string, SaveNode> _saveNodes = new Dictionary<string, SaveNode>();

        private void Start()
        {
            InitialSaveState = CreateSaveState("");
            foreach (var node in gameObject.GetComponentsInChildren<InteractionNode>())
            {
                node.Manager = this;
                switch (node)
                {
                    case StartNode startNode:
                        _startNode = startNode;
                        break;
                    case SaveNode saveNode:
                        if (saveNode.ID == "")
                        {
                            Debug.LogWarning("Ignoring non-set save node ID");
                        }
                        else
                        {
                            _saveNodes[saveNode.ID] = saveNode;
                        }

                        break;
                }
            }
        }

        [Header("Save Tracked Objects")] [SerializeField]
        private List<GameObject> _activeObjects;

        [SerializeField] private List<Behaviour> _enabledBehaviors;
        [SerializeField] private List<Renderer> _visibleRenderers;
        [SerializeField] private List<Transform> _savedTransforms;

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
                ActiveObjects = _activeObjects.Select(e => e != null && e.activeSelf).ToList(),
                EnabledBehaviors = _enabledBehaviors.Select(e => e != null && e.enabled).ToList(),
                VisibleRenderers = _visibleRenderers.Select(e => e != null && e.enabled).ToList(),
                SavedTransforms = _savedTransforms.Select(t =>
                    t == null
                        ? new SavedTransform()
                        : new SavedTransform
                        {
                            LocalPosition = t.localPosition,
                            LocalRotation = t.localRotation,
                            LocalScale = t.localScale,
                        }
                ).ToList()
            };
        }

        public void LoadSaveState(AbstractSaveState saveState)
        {
            ApplySaveState(saveState as SaveState ?? InitialSaveState);
        }

        private void ApplySaveState(SaveState state)
        {
            if (CurrentNode != null)
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

            for (var i = 0; i < Math.Min(_savedTransforms.Count, state.SavedTransforms.Count); i++)
            {
                _savedTransforms[i].localPosition = state.SavedTransforms[i].LocalPosition;
                _savedTransforms[i].localRotation = state.SavedTransforms[i].LocalRotation;
                _savedTransforms[i].localScale = state.SavedTransforms[i].LocalScale;
            }

            if (_saveNodes.ContainsKey(state.LatestSavePoint))
            {
                var next = _saveNodes[state.LatestSavePoint].Next;
                if (next != null)
                    next.EnterNode();
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
            public List<SavedTransform> SavedTransforms;

            public SaveState Copy()
            {
                return new SaveState
                {
                    LatestSavePoint = LatestSavePoint,
                    ActiveObjects = ActiveObjects,
                    EnabledBehaviors = EnabledBehaviors,
                    VisibleRenderers = VisibleRenderers,
                    SavedTransforms = SavedTransforms,
                };
            }
        }

        [XmlType("Transform")]
        public struct SavedTransform
        {
            public Vector3 LocalPosition;
            public Quaternion LocalRotation;
            public Vector3 LocalScale;
        }
    }
}