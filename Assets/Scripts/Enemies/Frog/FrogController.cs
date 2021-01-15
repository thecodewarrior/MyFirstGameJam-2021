using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{

    protected Rigidbody2D m_Rigidbody2D;
    protected Vector3 m_Velocity = Vector3.zero;
    protected Animator animator;
    protected DetectPlayer detectPlayer;
    protected bool isJumping;
    protected bool isFalling;
    protected float horizontalMoveDirection;
    protected float jumpStartTime;
    protected bool isAttacking;
    protected bool canAttack = true;
    protected bool isFacingLeft;
    protected PlayerMovement playerMovement;
    protected Vector3 previousPosition;

    [SerializeField] private Vector2 m_JumpForce;                          // Amount of force added when the player jumps.
   
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private float timeBetweenJumps;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool Grounded { get; private set; } = false;          // Whether or not the player is grounded.
    
   
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        detectPlayer = GetComponentInChildren<DetectPlayer>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isAttacking && canAttack)
        {
            canAttack = false;
            StartCoroutine(StartAttackCoolDown());
            MakeEnemyFacePlayer();
            Jump();
        }

        SetAttackState();
        SetAnimation();
    }

    IEnumerator StartAttackCoolDown()
    {
        yield return new WaitForSeconds(timeBetweenJumps);
        canAttack = true;
    }
    private void FixedUpdate()
    {

        
        Move(horizontalMoveDirection * Time.fixedDeltaTime, isJumping);

        CheckForGround();
        if(Grounded && isFalling)
        {
            m_Rigidbody2D.velocity = Vector2.zero;
        }
        isJumping = false;
        CheckIfFalling();
        previousPosition = transform.position;
    }

    public void CheckForGround()
    {
        bool wasGrounded = Grounded;
        Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
            }
        }
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

    public void SetAttackState()
    {
        if (detectPlayer.playerNear)
        {
            isAttacking = true;
        } else
        {
            isAttacking = false;
        }
    }

    public void SetAnimation()
    {
        animator.SetBool("isGrounded", Grounded);
        animator.SetBool("isFalling", isFalling);
    }

    public void Move(float move, bool jump)
    {
        
        // If the player should jump...
        if (Grounded && jump)
        {
            // Add a vertical force to the player.
            Grounded = false;
            if (isFacingLeft)
            {
                m_Rigidbody2D.AddForce(new Vector2(-m_JumpForce.x, m_JumpForce.y));
            } else
            {
                m_Rigidbody2D.AddForce(new Vector2(m_JumpForce.x, m_JumpForce.y));
            }
            
        }
    }

    public void Jump()
    {
        isJumping = true;
    }

    public void MakeEnemyFacePlayer()
    {
        if(playerMovement.gameObject.transform.position.x > transform.position.x)
        {
            FaceRight();
        } else
        {
            FaceLeft();
        }
    }

    public void FaceLeft()
    {
        isFacingLeft = true;
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    public void FaceRight()
    {
        isFacingLeft = false;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
