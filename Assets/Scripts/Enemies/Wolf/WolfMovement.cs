using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfMovement : MonoBehaviour
{

    private bool movingRight = false;

    protected bool isWalking;
    public Animator animator;

    public float speed;
    public float distance;
    public Transform groundDetection;
    public LayerMask whatIsGround;

    void Start()
    {

    }

    void Update()
    {

        if (isWalking)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        

        //RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, whatIsGround);
        //if(groundInfo.collider == false)
        //{
        //    if(movingRight == true)
        //    {
        //        MoveLeft();   
        //    } else
        //    {
        //        MoveRight();
        //    }
        //}
    }

    public void MoveRight()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        movingRight = true;
    }

    public void MoveLeft()
    {
        transform.eulerAngles = new Vector3(0, -180, 0);
        movingRight = false;
    }

    public void ChangeStateToIdle()
    {
        isWalking = false;
        animator.SetBool("isWalking", false);
    }

    public void ChangeStateToWalking()
    {
        isWalking = true;
        animator.SetBool("isWalking", true);
    }
}
