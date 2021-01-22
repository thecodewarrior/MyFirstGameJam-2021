using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{

    protected LevelManager levelManager;
    protected PlayerMovement playerMovement;
    protected Health playerHealth;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !playerMovement.isHiding && !playerMovement.isDead)
        {
            playerHealth.KillPlayer();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        levelManager = FindObjectOfType<LevelManager>();
        playerHealth = playerMovement.gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
