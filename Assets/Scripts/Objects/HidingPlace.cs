using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{

    public Color normalColor;
    public Color hiddenColor;

    protected PlayerMovement playerMovement;
    protected SpriteRenderer spriteRenderer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            HidePlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UnHidePlayer();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        spriteRenderer = playerMovement.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void HidePlayer()
    {
        spriteRenderer.color = hiddenColor;
        playerMovement.HidePlayer();
    }

    public virtual void UnHidePlayer()
    {
        spriteRenderer.color = normalColor;
        playerMovement.UnHidePlayer();
    }
}
