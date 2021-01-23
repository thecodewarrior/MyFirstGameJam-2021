using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{

    protected PlayerMovement playerMovement;
    protected DetectPlayer detectPlayer;
    protected bool isFacingLeft;
    protected bool isAttacking;
    protected bool playerNear;
    protected bool canAttack = true;
    protected BezierFollow bezierFollow;
    protected SpriteRenderer[] controlPointSpriteRenderers;
    protected Animator animator;
    protected float attackEndTime;
    protected AudioSource audioSource;

    public GameObject route;
    public float timeBetweenAttacks;
    public float timeBeforeDiveAnimation;

    // Start is called before the first frame update
    void Start()
    {
        detectPlayer = GetComponentInChildren<DetectPlayer>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        bezierFollow = GetComponent<BezierFollow>();
        controlPointSpriteRenderers = route.GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        TurnOffControlPoints();
        FaceLeft();
    }

    // Update is called once per frame
    void Update()
    {

        SetAttackState();

        if(playerNear && canAttack)
        {
            canAttack = false;
            StartAttack();
        }

        if (!isAttacking)
        {
            MakeEnemyFacePlayer();
        }

    }

    public void SetAttackState()
    {
        if (detectPlayer.playerNear)
        {
            playerNear = true;
        }
        else
        {
            playerNear = false;
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
        MakeEnemyFacePlayer();
        SetRoute();
        bezierFollow.coroutineAllowed = true;
        audioSource.Play();
        //Invoke("SetBatToDiving", timeBeforeDiveAnimation);
    }

    public void StopAttack()
    {
        isAttacking = false;
        //SetBatToFlying();
        StartCoroutine(StartAttackCoolDown());
    }

    private IEnumerator StartAttackCoolDown()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    public void SetRoute()
    {
        route.transform.position = transform.position;
        if (playerMovement.gameObject.transform.position.x > transform.position.x)
        {
            route.transform.localScale = new Vector3(Mathf.Abs(route.transform.localScale.x), route.transform.localScale.y, route.transform.localScale.z);
        }
        else
        {
            route.transform.localScale = new Vector3(-Mathf.Abs(route.transform.localScale.x), route.transform.localScale.y, route.transform.localScale.z);
        }
    }

    public void MakeEnemyFacePlayer()
    {
        if (playerMovement.gameObject.transform.position.x > transform.position.x)
        {
            FaceRight();
        }
        else
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

    public void TurnOffControlPoints()
    {
        for (int i = 0; i < controlPointSpriteRenderers.Length; i++)
        {
            controlPointSpriteRenderers[i].enabled = false;
        }
    }

    public void SetBatToFlying()
    {
        animator.SetBool("isDiving", false);
    }

    public void SetBatToDiving()
    {
        animator.SetBool("isDiving", true);
    }
}
