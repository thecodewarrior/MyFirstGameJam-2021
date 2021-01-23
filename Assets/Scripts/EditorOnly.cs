using UnityEngine;

public class EditorOnly : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_EDITOR
        Destroy(gameObject);
#endif
    }
}