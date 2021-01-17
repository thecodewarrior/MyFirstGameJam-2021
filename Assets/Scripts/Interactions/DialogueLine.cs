using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public Sprite Profile;

    [TextArea(3, 5)]
    public string Text;
}