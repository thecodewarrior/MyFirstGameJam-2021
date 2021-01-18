using System.Collections;
using Interactions.Actions;
using UnityEngine;
using UnityEngine.UIElements;

public class PopupDialogUIController : AbstractUIController
{
    protected override string TemplateName => "popup_dialog";
    
    protected PlayerMovement playerMovement;

    private int _currentPopupIndex;
    private ShowPopupAction _action;

    private Label _dialogText;

    void OnEnable()
    {
        _currentPopupIndex = 0;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    protected override void Bind()
    {
        _dialogText = Root.Q<Label>("dialog");
    }
    
    void Update()
    {
        if (Active && Input.GetButtonDown("Fire1"))
        {
            DisplayNextMessage();
        }
    }

    public void ShowPopup(ShowPopupAction action)
    {
        if (!Active)
            Manager.Push(this);

        playerMovement.FreezePlayer();
        
        _action = action;
        _currentPopupIndex = 0;
        DisplayNextMessage();
    }

    public void DisplayNextMessage()
    {
        if (_currentPopupIndex >= _action.Messages.Count)
        {
            EndPopup();
            return;
        }

        _dialogText.text = _action.Messages[_currentPopupIndex++].Text;
    }

    public void EndPopup()
    {
        if (Active)
            Manager.Pop();
        playerMovement.UnFreezePlayer();
        _action.EndPopup();
    }

    public void FastForwardDialogue()
    {
        StopAllCoroutines();
    }
}