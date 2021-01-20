using UnityEngine;

[AddComponentMenu("Note", 1)]
public class ObjectNote : MonoBehaviour
{
    [TextArea(4, 9)]
    public string Comment;
}