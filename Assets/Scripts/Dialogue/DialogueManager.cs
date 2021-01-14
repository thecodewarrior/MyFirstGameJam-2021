using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    protected Dialogue currentDialogue;
    protected int currentDialogueIndex;
    protected bool isTyping;
    protected PlayerMovement playerMovement;
    protected bool canInteract;
    [Header("Settings")]
    public float typeSpeed;

    [Header("Objects")]
    public Image dialogueImage;
    public Text dialogueText;
    public Animator dialogueBoxAnimator;
    public GameObject dialogueBoxHolder;
    public List<Dialogue> currentDialogueGroup = new List<Dialogue>();

    [Header("Sprites")]
    public Sprite playerNormalSprite;
    public Sprite wolfNormalSprite;

    void OnEnable()
    {
        dialogueBoxHolder.SetActive(false);
        dialogueText.text = "";
        currentDialogueIndex = 0;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canInteract)
        {
            if (isTyping)
            {   
                FastForwardDialogue();
            } else
            {
                DisplayNextSentence();
            }
        }
        

    }

    public void SwitchSprite(string spriteName)
    {
        switch (spriteName)
        {
            case "player_normal":
                dialogueImage.sprite = playerNormalSprite;
                break;
            case "wolf_normal":
                dialogueImage.sprite = wolfNormalSprite;
                break;
            default:
                break;
        }
    }

    public void StartDialogue()
    {
        currentDialogueIndex = 0;
        dialogueBoxHolder.SetActive(true);
        dialogueBoxAnimator.SetBool("isOpening", true);
        SwitchSprite(currentDialogueGroup[0].spriteName);
        dialogueText.text = "";
        Invoke("AllowInteraction", .6f);
        Invoke("DisplayNextSentence", .5f);
    }

    public void DisplayNextSentence()
    {
        if(currentDialogueIndex > currentDialogueGroup.Count - 1)
        {
            EndDialogue();
            return;
        }

        currentDialogue = currentDialogueGroup[currentDialogueIndex];

        SwitchSprite(currentDialogue.spriteName);
        StartCoroutine(TypeSentence(currentDialogue.sentence));

        currentDialogueIndex++;
    }

    public void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("isOpening", false);
        PreventInteraction();
        Invoke("DeactivateDialogueAndAllowPlayerMovement", .5f);
    }

    public void DeactivateDialogueAndAllowPlayerMovement()
    {
        dialogueBoxHolder.SetActive(false);
        playerMovement.UnFreezePlayer();
    }

    public IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        char[] letters = sentence.ToCharArray();
        
        for (int i = 0; i < letters.Length; i++)
        {
            int characterIndex = i;
            string text = sentence.Substring(0, characterIndex);
            
            text += "<color=#00000000>" + sentence.Substring(characterIndex) + "</color>";
            
            dialogueText.text = text;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    public void FastForwardDialogue()
    {
        StopAllCoroutines();
        isTyping = false;
        dialogueText.text = currentDialogue.sentence;
    }

    public void AllowInteraction()
    {
        canInteract = true;
    }

    public void PreventInteraction()
    {
        canInteract = false;
    }
}
