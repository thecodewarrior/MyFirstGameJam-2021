using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSFXManager : MonoBehaviour
{

    protected bool playerIsNear;
    protected AudioSource audioSource;
    protected Health playerHealth;
    protected Transform playerTransform;
    public int playerLocation;
    protected int previousPlayerLocation;
    protected bool isTransitioningLouder;
    protected bool isTransitioningSofter;
    protected bool isPanningLeft;
    protected bool isPanningRight;
    protected bool isPanningCenter;

    public float distanceToPlayMonoSFX;
    public float farVolume;
    public float closeVolume;
    public float transitionTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIsNear = false;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerHealth = FindObjectOfType<Health>();
        playerTransform = playerHealth.gameObject.transform;
        audioSource.volume = farVolume;
    }

    // Update is called once per frame
    void Update()
    {

        LocatePlayer();

        if (playerIsNear && !audioSource.isPlaying)
        {
            StopAllCoroutines();
            SetInitialVolumeAndPan();
            audioSource.Play();
        } else if (!playerIsNear && audioSource.isPlaying)
        {
            StartCoroutine(FadeOutVolume());
        }

        if (audioSource.isPlaying)
        {
            ChangeVolume();
            ChangeSteroPan();
        }

        CheckPlayerDead();

        previousPlayerLocation = playerLocation;
    }

    public void SetInitialVolumeAndPan()
    {
        audioSource.volume = farVolume;
        if (playerLocation == 0)
        {
            audioSource.panStereo = 1f;
        }
        else if (playerLocation == 2)
        {
            audioSource.panStereo = -1f;
        }
    }

    public int LocatePlayer()
    {
        if(playerTransform.position.x > transform.position.x && Mathf.Abs(playerTransform.position.x - transform.position.x) > distanceToPlayMonoSFX)
        {
            playerLocation = 2;
            return playerLocation;
        }
        if(playerTransform.position.x < transform.position.x && Mathf.Abs(playerTransform.position.x - transform.position.x) > distanceToPlayMonoSFX)
        {
            playerLocation = 0;
            return playerLocation;
        }

        playerLocation = 1;

        return playerLocation;

    }

    public void ChangeSteroPan()
    {
        if (previousPlayerLocation == 0 && playerLocation == 1 || previousPlayerLocation == 2 && playerLocation == 1)
        {
            isPanningCenter = true;
        }

        if (previousPlayerLocation == 1 && playerLocation == 0)
        {
            isPanningRight = true;
        }

        if (previousPlayerLocation == 1 && playerLocation == 2)
        {
            isPanningLeft = true;
        }

        if (isPanningCenter)
        {
            audioSource.panStereo = Mathf.Lerp(audioSource.panStereo,0f, transitionTime * Time.deltaTime);
            if (Mathf.Abs(audioSource.panStereo) < 0.05)
            {
                audioSource.panStereo = 0;
                isPanningCenter = false;
            }
        }

        if (isPanningRight)
        {
            audioSource.panStereo = Mathf.Lerp(audioSource.panStereo, 1f, transitionTime * Time.deltaTime);
            if (Mathf.Abs(audioSource.panStereo - 1) < 0.05)
            {
                audioSource.panStereo = 1;
                isPanningRight = false;
            }
        }

        if (isPanningLeft)
        {
            audioSource.panStereo = Mathf.Lerp(audioSource.panStereo, -1f, transitionTime * Time.deltaTime);
            if (Mathf.Abs(1 + audioSource.panStereo) < 0.05)
            {
                audioSource.panStereo = -1;
                isPanningLeft = false;
            }
        }

    }

    public void ChangeVolume()
    {
        if(previousPlayerLocation == 0 && playerLocation == 1 || previousPlayerLocation == 2 && playerLocation == 1)
        {
            isTransitioningLouder = true;
        }

        if(previousPlayerLocation == 1 && playerLocation == 0 || previousPlayerLocation == 1 && playerLocation == 2)
        {
            isTransitioningSofter = true;
        }

        if (isTransitioningLouder)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, closeVolume, transitionTime * Time.deltaTime);
            if(audioSource.volume > closeVolume)
            {
                isTransitioningLouder = false;
            }
        }

        if (isTransitioningSofter)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, farVolume, transitionTime * Time.deltaTime);
            if (audioSource.volume < farVolume)
            {
                isTransitioningSofter = false;
            }
        }
        
    }

    public void ChangeSound(string soundPlacement)
    {
        switch (soundPlacement)
        {
            case "right":
                audioSource.panStereo = 1f;
                audioSource.volume = farVolume;
                break;
            case "center":
                audioSource.panStereo = 0;
                audioSource.volume = closeVolume;
                break;
            case "left":
                audioSource.panStereo = -1f;
                audioSource.volume = farVolume;
                break;
           
            default:
                break;
        }
    }

    public IEnumerator FadeOutVolume()
    {
        audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, transitionTime * Time.deltaTime);
        yield return new WaitForSeconds(0.1f);
        if (audioSource.volume > 0.05f)
        {
            StartCoroutine(FadeOutVolume());
        } else
        {
            audioSource.volume = 0;
            audioSource.Stop();
        }
    }

    public void CheckPlayerDead()
    {
        if (playerHealth.isDead)
        {
            audioSource.Stop();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 startPosition = new Vector3(transform.position.x - distanceToPlayMonoSFX, transform.position.y, transform.position.z);
        Vector3 endPosition = new Vector3(transform.position.x + distanceToPlayMonoSFX, transform.position.y, transform.position.z);
        Gizmos.DrawLine(startPosition, endPosition);
    }
}
