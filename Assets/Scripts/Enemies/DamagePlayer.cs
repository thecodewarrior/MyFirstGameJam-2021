using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    protected Health playerHealth;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth.DamagePlayer();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<Health>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
