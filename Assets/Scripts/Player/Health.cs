using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    protected PlayerMovement playerMovement;
    protected AudioSource myAudioSource;
    protected LevelManager levelManager;

    public AudioClip damageAudioClip;
    public Color damageColor;
    public float invincibilityTime;
    public float filckerInterval;
    public bool isInvunerable;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        myAudioSource = GetComponent<AudioSource>();
        levelManager = FindObjectOfType<LevelManager>();
        myAudioSource.clip = damageAudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer()
    {
        if (isInvunerable)
        {
            return;
        }

       

        if(GameManager.instance.currentHealth > 0)
        {
            //myAudioSource.Play();
            GameManager.instance.ReduceHealth();
            isInvunerable = true;
            Invoke("MakePlayerVulnerable", invincibilityTime);
            StartCoroutine("FlickerSprite");
        }
        else
        {
            levelManager.KillPlayer();
        }
        
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
}
