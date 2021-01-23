using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundTypeCheck : MonoBehaviour
{
    [NonSerialized] public List<StepMaterial> StepMaterialStack = new List<StepMaterial>();

    public StepMaterial Material
    {
        get
        {
            if (StepMaterialStack.Count == 0)
                return null;

            return StepMaterialStack.Last();
        }
    }
}