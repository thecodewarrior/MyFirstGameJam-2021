using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    private LevelManager levelManager;
    private DeathController DeathController;
    private Animator animator;

    public GameObject hurtAudio;
    public Color damageColor;
    public float invincibilityTime;
    public float filckerInterval;
    public bool isInvunerable;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        levelManager = FindObjectOfType<LevelManager>();
        DeathController = FindObjectOfType<DeathController>();
        animator = GetComponent<Animator>();
    }

    public void DamagePlayer()
    {
        if (isInvunerable || GlobalPlayerData.Health <= 0)
        {
            return;
        }

        PlayDamageSound();

        GlobalPlayerData.DoDamage(1);

        if(GlobalPlayerData.Health <= 0)
        {
            KillPlayer();
            Invoke("ShowDeathScene", 1f);
        }
        else
        {
            isInvunerable = true;
            Invoke("MakePlayerVulnerable", invincibilityTime);
            StartCoroutine("FlickerSprite");
        }
        
    }

    public void KillPlayer()
    {
        isDead = true;
        playerMovement.isDead = true;
        animator.SetBool("isDead", true);
    }

    public void ShowDeathScene()
    {
        DeathController.ShowDeath();
    }

    public void InstantKillPlayer()
    {
        KillPlayer();
        ShowDeathScene();
    }

    public void RevivePlayer()
    {
        isDead = false;
        playerMovement.isDead = false;
        animator.SetBool("isDead", false);
    }

    public void MakePlayerVulnerable()
    {
        isInvunerable = false;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public IEnumerator FlickerSprite()
    {
        spriteRenderer.color = damageColor;

        yield return new WaitForSeconds(filckerInterval);

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        yield return new WaitForSeconds(filckerInterval);

        if (isInvunerable)
        {
            StartCoroutine("FlickerSprite");
        }
    }

    public void PlayDamageSound()
    {
        Instantiate(hurtAudio, transform.position, transform.rotation);
    }
}
