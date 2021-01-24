using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public Sprite Portrait;

    public OptionalFacingDirection PlayerFacing;

    [TextArea(3, 5)] public string Text;
}

public enum OptionalFacingDirection
{
    None, Left, Right
}
