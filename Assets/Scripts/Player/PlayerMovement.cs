using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    protected CharacterController2D controller;
    protected bool isJumping = false;
    protected Animator animator;
    protected Vector3 previousPosition;
    public bool isDead;
    public bool isHiding { get; private set; } = false;
    public bool isCrouching { get; private set; } = false;
    public bool isFrozen { get; private set; } = false;
    public bool isFalling { get; private set; } = false;
    public float runSpeed = 40f;

    float horizontalMove = 0f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.HasInputFocus)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
            }


            if (Input.GetAxisRaw("Vertical") < 0)
            {
                isCrouching = true;
            }
            else
            {
                isCrouching = false;
            }
        }

        SetAnimation();
    }

    void FixedUpdate()
    {
        if (!isFrozen)
        {
            //Move our character
            controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        }
        else
        {
            controller.FreezePlayerMovement();
        }

        isJumping = false;
        CheckIfFalling();
        previousPosition = transform.position;
    }

    public void CheckIfFalling()
    {
        if (transform.position.y < previousPosition.y)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    public void SetAnimation()
    {
        animator.SetBool("grounded", controller.Grounded);
        animator.SetBool("isCrouching", isCrouching);
        animator.SetBool("isFalling", isFalling);
        animator.SetFloat("horizontalAxis", Mathf.Abs(horizontalMove));

        if (isFrozen)
        {
            animator.SetFloat("horizontalAxis", Mathf.Abs(0f));
            animator.SetBool("isCrouching", false);
        }
    }

    public void FreezePlayer()
    {
        isFrozen = true;
    }

    public void UnFreezePlayer()
    {
        isFrozen = false;
    }

    public void MakePlayerFaceRight()
    {
        controller.MakePlayerFaceRight();
    }

    public void MakePlayerFaceLeft()
    {
        controller.MakePlayerFaceLeft();
    }

    public void HidePlayer()
    {
        isHiding = true;
    }

    public void UnHidePlayer()
    {
        isHiding = false;
    }
}