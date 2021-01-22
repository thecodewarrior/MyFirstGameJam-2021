using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    protected PlayerMovement playerMovement;
    protected LevelManager levelManager;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
        levelManager.KillPlayer();
        print("player is dead");
    }

    public void RevivePlayer()
    {
        isDead = false;
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
