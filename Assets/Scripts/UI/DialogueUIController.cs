using System.Collections;
using Interactions.Actions;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueUIController : AbstractUIController
{
    protected override string TemplateName => "dialogue";
    
    protected bool isTyping;
    protected PlayerMovement playerMovement;
    [Header("Settings")] public float typeSpeed;

    private DialogueLine _currentDialogueLine;
    private int _currentDialogueIndex;
    private StartDialogueAction _action;

    private VisualElement _profile;
    private Label _dialogueText;

    void OnEnable()
    {
        _currentDialogueIndex = 0;
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
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    public void SwitchSprite(Sprite sprite)
    {
        _profile.style.backgroundImage = new StyleBackground(sprite);
    }

    public void StartDialogue(StartDialogueAction action)
    {
        if (!Active)
            Manager.Push(this);

        playerMovement.FreezePlayer();
        
        _action = action;
        _currentDialogueIndex = 0;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_currentDialogueIndex > _action.Dialogue.Count - 1)
        {
            EndDialogue();
            return;
        }

        _currentDialogueLine = _action.Dialogue[_currentDialogueIndex];

        _dialogueText.text = "";
        SwitchSprite(_currentDialogueLine.Portrait);
        StartCoroutine(TypeSentence(_currentDialogueLine.Text));

        _currentDialogueIndex++;
    }

    public void EndDialogue()
    {
        if (Active)
            Manager.Pop();
        playerMovement.UnFreezePlayer();
        _action.EndDialogue();
    }

    public IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;

        for (int i = 0; i <= sentence.Length; i++)
        {
            _dialogueText.text = sentence.Substring(0, i);
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    public void FastForwardDialogue()
    {
        StopAllCoroutines();
        isTyping = false;
        _dialogueText.text = _currentDialogueLine.Text;
    }
}