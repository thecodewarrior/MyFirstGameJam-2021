using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float speed;
    public bool moveLeft;

    void Start()
    {
        if (moveLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
