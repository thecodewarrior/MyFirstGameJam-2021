using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    protected CharacterController2D controller;
    protected bool isJumping = false;
    protected Animator animator;
    protected Vector3 previousPosition;
    protected AudioSource audioSource;
    protected bool isGrounded;
    protected bool previousIsGround;

    public bool isDead;
    public bool isHiding { get; private set; } = false;
    public bool isCrouching { get; private set; } = false;
    public bool isFrozen { get; private set; } = false;
    public bool isFalling { get; private set; } = false;
    public float runSpeed = 40f;

    protected WolfWakeGround wolfWakeGround;
    
    float horizontalMove = 0f;

    public FacingDirection Facing
    {
        get => controller.FacingRight ? FacingDirection.Right : FacingDirection.Left;
        set
        {
            if (value == FacingDirection.Right)
            {
                controller.MakePlayerFaceRight();
            }
            else
            {
                controller.MakePlayerFaceLeft();
            }
        }
    }

    [NonSerialized] public List<StepMaterial> StepMaterialStack = new List<StepMaterial>();
    public StepMaterial StepMaterial
    {
        get
        {
            if (StepMaterialStack.Count == 0)
                return null;

            return StepMaterialStack.Last();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        wolfWakeGround = FindObjectOfType<WolfWakeGround>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.Grounded;

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
        if (CheckJustLanded() && horizontalMove < .01f)
        {
            PlayFootStepSFX();
            AddToWolfWakeMeter();
        }


        previousIsGround = isGrounded;
    }

    void FixedUpdate()
    {
        if (isFrozen || isDead)
        {
            controller.FreezePlayerMovement();
        }
        else
        {
            //Move our character
            controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
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

        if (isFrozen || isDead)
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

    public bool CheckJustLanded()
    {
        if (previousIsGround == false && isGrounded == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayFootStepSFX()
    {
        if (StepMaterial == null || StepMaterial.StepSFX.Count == 0)
            return; // edge case

        audioSource.clip = CheckJustLanded()
            ? StepMaterial.LandSFX
            : StepMaterial.StepSFX[Random.Range(0, StepMaterial.StepSFX.Count)];

        audioSource.Play();
    }

    public void AddToWolfWakeMeter()
    {
        if (wolfWakeGround != null)
        {
            if (StepMaterial != null && StepMaterial.alertsWolf)
            {
                wolfWakeGround.AddToWolfWakeMeter();
            }
        }
    }

    public void WakeWolf()
    {
        if (wolfWakeGround != null)
        {
            if (StepMaterial != null && StepMaterial.alertsWolf)
            {
                wolfWakeGround.WakeWolf();
            }
        }
    }
}

public enum FacingDirection
{
    Left,
    Right
}