using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    protected DialogueManager dialogueManager;
    protected bool hasBeenTriggered;
    protected bool canBeTriggered;
    protected PlayerMovement playerMovement;

    public GameObject NPC;
    public bool automaticTrigger;
    public List<Dialogue> dialogueLines = new List<Dialogue>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
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
        if (collision.tag == "Player")
        {
            canBeTriggered = false;
        }
    }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (canBeTriggered && !automaticTrigger && dialogueManager.dialogueBoxHolder.activeSelf == false)
            {
                TriggerDialogue();
                MakePlayerFaceNPC();
                playerMovement.FreezePlayer();
                
            }
        }
    }

    public void TriggerDialogue()
    {
        dialogueManager.currentDialogueGroup = dialogueLines;
        dialogueManager.StartDialogue();
    }

    public void MakePlayerFaceNPC()
    {
        if (NPC.transform.position.x > playerMovement.gameObject.transform.position.x)
        {
            playerMovement.MakePlayerFaceRight();
        } else
        {
            playerMovement.MakePlayerFaceLeft();
        }
    }
}
