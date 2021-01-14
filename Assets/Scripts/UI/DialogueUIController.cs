using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueUIController : AbstractUIController
{
    protected Dialogue currentDialogue;
    protected int currentDialogueIndex;
    protected bool isTyping;
    protected PlayerMovement playerMovement;
    [Header("Settings")]
    public float typeSpeed;

    public List<Dialogue> currentDialogueGroup = new List<Dialogue>();

    [Header("Sprites")]
    public Sprite playerNormalSprite;
    public Sprite wolfNormalSprite;
    
    private VisualElement _profile;
    private Label _dialogueText;

    void OnEnable()
    {
        currentDialogueIndex = 0;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    protected override void Bind()
    {
        _dialogueText = Root.Q<Label>("dialogue");
        _profile = Root.Q("profile");
    }

    void Update()
    {
        if (Active && Input.GetButtonDown("Fire1"))
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
                _profile.style.backgroundImage = new StyleBackground(playerNormalSprite);
                break;
            case "wolf_normal":
                _profile.style.backgroundImage = new StyleBackground(wolfNormalSprite);
                break;
        }
    }

    public void StartDialogue()
    {
        if(!Active)
            Manager.Push(this);
        currentDialogueIndex = 0;
        SwitchSprite(currentDialogueGroup[0].spriteName);
        _dialogueText.text = "";
        DisplayNextSentence();
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
        if(Active)
            Manager.Pop();
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
            
            _dialogueText.text = text;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    public void FastForwardDialogue()
    {
        StopAllCoroutines();
        isTyping = false;
        _dialogueText.text = currentDialogue.sentence;
    }
}