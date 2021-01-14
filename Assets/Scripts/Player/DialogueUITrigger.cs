using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUITrigger : MonoBehaviour
{
    protected DialogueUIController _dialogueController;
    protected bool hasBeenTriggered;
    protected bool canBeTriggered;
    protected PlayerMovement playerMovement;

    public GameObject FocusPoint;
    public bool automaticTrigger;
    public List<Dialogue> dialogueLines = new List<Dialogue>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (automaticTrigger && !hasBeenTriggered)
            {
                TriggerDialogue();
                MakePlayerFaceNPC();
                playerMovement.FreezePlayer();
                hasBeenTriggered = true;
            }

            canBeTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canBeTriggered = false;
        }
    }

    void Start()
    {
        _dialogueController = FindObjectOfType<DialogueUIController>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (canBeTriggered && !automaticTrigger && !_dialogueController.Active)
            {
                TriggerDialogue();
                MakePlayerFaceNPC();
                playerMovement.FreezePlayer();
            }
        }
    }

    public void TriggerDialogue()
    {
        _dialogueController.currentDialogueGroup = dialogueLines;
        _dialogueController.StartDialogue();
    }

    public void MakePlayerFaceNPC()
    {
        if (FocusPoint.transform.position.x > playerMovement.gameObject.transform.position.x)
        {
            playerMovement.MakePlayerFaceRight();
        } else
        {
            playerMovement.MakePlayerFaceLeft();
        }
    }
}
