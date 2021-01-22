using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    protected CharacterController2D controller;
    protected bool isJumping = false;
    protected Animator animator;
    protected Vector3 previousPosition;
    protected GroundTypeCheck groundTypeCheck;
    protected AudioSource audioSource;
    protected bool isGrounded;
    protected bool previousIsGround;
    protected bool onWolfWakeGround;

    public string currentGroundType;

    public bool isDead;
    public bool isHiding { get; private set; } = false;
    public bool isCrouching { get; private set; } = false;
    public bool isFrozen { get; private set; } = false;
    public bool isFalling { get; private set; } = false;
    public float runSpeed = 40f;
    public AudioClip[] grassSFX;
    public AudioClip[] woodSFX;
    public AudioClip[] swampSFX;
    public AudioClip[] stoneSFX;
    protected WolfWakeGround wolfWakeGround;

    float horizontalMove = 0f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        groundTypeCheck = GetComponentInChildren<GroundTypeCheck>();
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
        SetGroundType();
        CheckOnWolfWakeGround();
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

    public void SetGroundType()
    {
        if(groundTypeCheck.groundType != null)
        {
            currentGroundType = groundTypeCheck.groundType;
        }  
    }

    public void CheckOnWolfWakeGround()
    {
        onWolfWakeGround = groundTypeCheck.isOnWolfWakeGround;
    }

    public bool CheckJustLanded()
    {
        if (previousIsGround == false && isGrounded == true)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void PlayFootStepSFX()
    {
        int footStepNumber;

        switch (currentGroundType)
        {
            case "Grass":
                if (CheckJustLanded())
                {
                    footStepNumber = 0;
                } else
                {
                    footStepNumber = Random.Range(0, grassSFX.Length);
                }
                
                audioSource.clip = grassSFX[footStepNumber];
                break;
            case "Wood":
                if (CheckJustLanded())
                {
                    footStepNumber = 0;
                }
                else
                {
                    footStepNumber = Random.Range(0, woodSFX.Length);
                }
                audioSource.clip = woodSFX[footStepNumber];
                break;
            case "Stone":
                if (CheckJustLanded())
                {
                    footStepNumber = 0;
                }
                else
                {
                    footStepNumber = Random.Range(0, stoneSFX.Length);
                }
                audioSource.clip = stoneSFX[footStepNumber];
                break;
            case "Swamp":
                if (CheckJustLanded())
                {
                    footStepNumber = 0;
                }
                else
                {
                    footStepNumber = Random.Range(0, swampSFX.Length);
                }
                audioSource.clip = swampSFX[footStepNumber];
                break;
            default:
                break;
        }

        audioSource.Play();
    }

    public void AddToWolfWakeMeter()
    {
        if(wolfWakeGround != null)
        {
            if (onWolfWakeGround)
            {
                wolfWakeGround.AddToWolfWakeMeter();
            }
        }
    }

    public void WakeWolf()
    {
        if(wolfWakeGround != null)
        {
            if (onWolfWakeGround)
            {
                wolfWakeGround.WakeWolf();
            }
        }
    }
}