using UnityEngine;
using System.Collections;

public class WolfMovement : MonoBehaviour
{
    public Vector3 pointB;

    protected Vector3 pointA;

    void Start()
    {
        pointA = transform.position;
        pointB.y = pointA.y;
        StartCoroutine(StartMoving());
    }

    IEnumerator StartMoving()
    {
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            var t = transform.localScale;
            t.x = -1;
            transform.localScale = t;
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
            t = transform.localScale;
            t.x = 1;
            transform.localScale = t;
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
