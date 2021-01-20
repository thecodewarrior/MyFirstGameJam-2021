using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Interactions
{
    [AddComponentMenu("Interaction/Start")]
    public class StartNode : InteractionNode
    {
        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            AdvanceTo(Next);
        }
    }
}