using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueUITrigger : MonoBehaviour
{
    protected DialogueUIController _dialogueController;
    protected bool hasBeenTriggered;
    protected bool canBeTriggered;
    protected PlayerMovement playerMovement;

    public bool NPCShouldFacePlayer;
    [FormerlySerializedAs("FocusPoint")] public GameObject NPC;
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
                if (NPCShouldFacePlayer)
                {
                    MakeNPCFacePlayer();
                }
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
            if (canBeTriggered && !automaticTrigger && !UIManager.HasInputFocus)
            {
                TriggerDialogue();
                MakePlayerFaceNPC();
                playerMovement.FreezePlayer();
            }
        }
    }

    public void TriggerDialogue()
    {
        // _dialogueController.currentDialogueGroup = dialogueLines;
        // _dialogueController.StartDialogue();
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
    
    public void MakeNPCFacePlayer()
    {
        if (NPC.transform.position.x > playerMovement.gameObject.transform.position.x)
        {
            NPC.transform.localScale = new Vector3(Mathf.Abs(NPC.transform.localScale.x), NPC.transform.localScale.y, NPC.transform.localScale.z);
        }
        else
        {
            NPC.transform.localScale = new Vector3(-Mathf.Abs(NPC.transform.localScale.x), NPC.transform.localScale.y, NPC.transform.localScale.z);
        }
    }
}
