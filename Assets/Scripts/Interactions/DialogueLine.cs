using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public Sprite Portrait;

    [TextArea(3, 5)]
    public string Text;
}