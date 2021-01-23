using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    private WolfMovement wolfMovement;
    private float timeBetweenActivation;
    private GameObject destination;
    private DetectPlayer detectPlayer;
    private string previousMusicPlaying;
    private bool wolfIsActive;
    private bool isFadingMusic;
    private DetectPlayerForAction detectPlayerForAction;
    private bool actionTried;
    private AudioSource audioSource;
    private Health playerHealth;
    public bool placeWolfAtEastPoint;

    public int actionNumber;

    public GameObject westPoint;
    public GameObject eastPoint;
    public GameObject wolf;
    
    public float timeBetweenActivationMin;
    public float timeBetweenActivationMax;
    public bool doNotReactivate;
    public bool alwaysGoForward;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<Health>();
        wolfMovement = wolf.GetComponent<WolfMovement>();
        detectPlayer = GetComponentInChildren<DetectPlayer>();
        detectPlayerForAction = GetComponentInChildren<DetectPlayerForAction>();
        audioSource = GetComponent<AudioSource>();

        if(!alwaysGoForward)
            SetRandomizeWolfPlacement();

        if (placeWolfAtEastPoint)
        {
            StartWolfAtEast();
        } else
        {
            StartWolfAtWest();
        }

        DeactivateWolf();
        SetTimeBetweenActivationTime();
        Invoke("ActivateWolf", timeBetweenActivation);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckWolfReachedDestination())
        {
            StopWolf();
            if (!doNotReactivate)
            {
                RestartWolfSequence();
            }
            
        }

        if (detectPlayerForAction.playerCollided && !actionTried)
        {
            actionTried = true;
            TryWolfAction();
        }


        // Used to play or stop wolf music when player is near.
        if (detectPlayer.playerNear)
        {
            if(SoundManager.instance.currentMusicPlaying != "wolf_music" && !playerHealth.isDead)
            {
                isFadingMusic = false;
                SoundManager.instance.StopFadeOut();
                PlayWolfMusic();
            }
        }
        else
        {
            if (SoundManager.instance.currentMusicPlaying == "wolf_music" && !isFadingMusic && !playerHealth.isDead)
            {
                isFadingMusic = true;
                SoundManager.instance.FadeOutSound("wolf_music", 1f);
                Invoke("PlayPreviousMusic", 1f);
            }
        }
    }

    public void TryWolfAction()
    {
        actionNumber = Random.Range(0, 10);

        if (actionNumber < 2)
        {
            //Do Nothing
            return;
        }
        else if (actionNumber >= 2 && actionNumber < 6 || alwaysGoForward)
        {
            wolfMovement.ChangeStateToIdle();
            Invoke("ContinueWalking", 3f);
        }
        else if (actionNumber >= 6 && actionNumber < 10)
        {
            wolfMovement.ChangeStateToIdle();
            Invoke("TurnAround", 3f);
        }
    }

    public void StartWolfAtWest()
    {
        wolf.transform.position = westPoint.transform.position;
        destination = eastPoint;
    }

    public void StartWolfAtEast()
    {
        wolf.transform.position = eastPoint.transform.position;
        destination = westPoint;
    }

    public void ActivateWolf()
    {
        previousMusicPlaying = SoundManager.instance.currentMusicPlaying;
        wolf.SetActive(true);
        PlayWolfHowl();
        if (destination == westPoint)
        {
            wolfMovement.ChangeStateToWalking();
            wolfMovement.MoveLeft();
        } else
        {
            wolfMovement.ChangeStateToWalking();
            wolfMovement.MoveRight();
        }
    }

    public void TurnAround()
    {
        if (destination == westPoint)
        {
            destination = eastPoint;
            wolfMovement.ChangeStateToWalking();
            wolfMovement.MoveRight();
        }
        else
        {
            destination = westPoint;
            wolfMovement.ChangeStateToWalking();
            wolfMovement.MoveLeft();
        }
    }

    public void ContinueWalking()
    {
        wolfMovement.ChangeStateToWalking();
    }

    public void DeactivateWolf()
    {
        wolf.SetActive(false);
    }

    public void SetTimeBetweenActivationTime()
    {
        timeBetweenActivation = Random.Range(timeBetweenActivationMin, timeBetweenActivationMax);
    }

    public bool CheckWolfReachedDestination()
    {
        if (destination == eastPoint)
        {
            if(wolf.transform.position.x > eastPoint.transform.position.x)
            {
                return true;
            }
        }

        if (destination == westPoint)
        {
            if (wolf.transform.position.x < westPoint.transform.position.x)
            {
                return true;
            }
        }

        return false;
    }

    public void StopWolf()
    {
        DeactivateWolf();
        if(SoundManager.instance.currentMusicPlaying == "wolf_music")
        {
            SoundManager.instance.FadeOutSound("wolf_music", 1f);
            Invoke("PlayPreviousMusic", 1f);
        }
        
    }

    public void RestartWolfSequence()
    {
        if (destination == eastPoint)
        {
            StartWolfAtEast();
        }
        else
        {
            StartWolfAtWest();
        }

        actionTried = false;
        detectPlayerForAction.playerCollided = false;
        SetTimeBetweenActivationTime();
        Invoke("ActivateWolf", timeBetweenActivation);
    }

    public void PlayWolfMusic()
    {
        SoundManager.instance.FadeOutSound(SoundManager.instance.currentMusicPlaying, 0f);
        SoundManager.instance.PlayMusic("wolf_music");
    }

    public void PlayPreviousMusic()
    {
        if (SoundManager.instance.currentMusicPlaying != previousMusicPlaying)
        {
            isFadingMusic = false;
            SoundManager.instance.PlayMusic(previousMusicPlaying);
        }     
    }

    public void PlayWolfHowl()
    {
        audioSource.Play();
    }

    public void SetRandomizeWolfPlacement()
    {
        int randomNumber = Random.Range(0, 2);
        if(randomNumber == 0)
        {
            placeWolfAtEastPoint = false;
        } else
        {
            placeWolfAtEastPoint = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(eastPoint.transform.position, .5f);
        Gizmos.DrawSphere(westPoint.transform.position, .5f);
    }
}
