using UnityEngine;

[AddComponentMenu("Note", 1)]
public class ObjectNote : MonoBehaviour
{
    [TextArea(2, 7)]
    public string Comment;
}