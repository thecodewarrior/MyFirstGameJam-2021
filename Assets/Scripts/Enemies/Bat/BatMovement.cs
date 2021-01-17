using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : BezierFollow
{

    protected BatController batController;

    void Start()
    {
        batController = GetComponent<BatController>();
        routeToGo = 0;
        tParam = 0f;
    }

    protected override IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            targetPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = targetPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo++;

        if (routeToGo > routes.Length - 1)
        {
            if (loopMovement)
            {
                routeToGo = 0;
                coroutineAllowed = true;
            }
            else
            {
                routeToGo = 0;
                print("stopping attack");
                batController.StopAttack();
            }

        }
        else
        {
            coroutineAllowed = true;
        }
    }
}
