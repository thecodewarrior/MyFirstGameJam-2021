using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public string startPointName;
    public int startingHealth;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth()
    {
        currentHealth--;
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

}
