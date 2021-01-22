using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LockPickPuzzleController : MonoBehaviour
{

    protected int currentPuzzle;
    protected int solutionIndex;
    protected int toolToMove;
    protected string currentAnswer;
    protected AudioSource audioSource;

    public string[] solutions;

    protected string[] currentSolutionArray;

    [Header("Objects")]
    public Button[] puzzleButtons;
    public Button startButton;
    public Button quitButton;
    public LockPickButton[] lockPickButtons;
    public Animator toolButton01Animator;
    public Animator toolButton02Animator;
    public Animator toolButton03Animator;
    public Text resultText;
    public Text puzzleNumberText;

    [Header("Settings")]
    public float timeBetweenMovements;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        currentPuzzle = 0;
        ConvertCurrentSolutionToArray();
        ResetCurrentPuzzle();
    }
    
    public void HandlePlayerInput(int toolNumber)
    {
        StartCoroutine(InputNumber(toolNumber));
    }

    public IEnumerator InputNumber(int toolNumber)
    {
        DeactivatePuzzleButtons();
        HighlightPuzzleButton(toolNumber);
        toolToMove = toolNumber;
        MoveTool();
        yield return new WaitForSeconds(timeBetweenMovements);
        ResetAnimators();
        if(currentAnswer.Length < solutions[currentPuzzle].Length)
        {
            currentAnswer += toolNumber.ToString();
            ActivatePuzzleButtons();
            FocusOnButton(toolNumber);
        }

        if(currentAnswer.Length == solutions[currentPuzzle].Length)
        {
            AttemptToSolvePuzzle();
        }
    }

    public void AttemptToSolvePuzzle()
    {
        resultText.enabled = true;
        DeactivatePuzzleButtons();
        HighlightAllPuzzleButtons();
        if (currentAnswer == solutions[currentPuzzle])
        {
            resultText.text = "Success!";
            Invoke("MoveToNextPuzzle", 2f);
        }
        else
        {
            resultText.text = "Fail!";
            Invoke("ResetCurrentPuzzle", 2f);
        }
    } 

    public void MoveToNextPuzzle()
    {
        currentPuzzle++;
        if(currentPuzzle >= solutions.Length)
        {
            UnlockDoor();
            return;
        }
        puzzleNumberText.text = (currentPuzzle + 1).ToString();
        ConvertCurrentSolutionToArray();
        ResetCurrentPuzzle();
    }

    public void UnlockDoor()
    {
        resultText.text = "Door Unlocked!";
    }

    public void StartShowSolution()
    {
        DeactivatePuzzleButtons();
        DeactivateStartQuitButton();
        StartCoroutine(ActivateTool());
        resultText.enabled = false;
    }

    private IEnumerator ActivateTool()
    {
        yield return new WaitForSeconds(timeBetweenMovements);

        SetToolToMove();
        MoveTool();

        yield return new WaitForSeconds(timeBetweenMovements);

        solutionIndex++;

        if (solutionIndex > currentSolutionArray.Length - 1)
        {
            StopShowingSolution();
            ResetAnimators();
        }
        else
        {
            ResetAnimators();
            StartCoroutine(ActivateTool());
        }
    }

    public void SetToolToMove()
    {
        toolToMove = Convert.ToInt32(currentSolutionArray[solutionIndex]);
    }

    public void MoveTool()
    {
        PlayLockPickSFX();
        switch (toolToMove)
        {
            case 0:
                toolButton01Animator.SetBool("isMoving", true);
                break;
            case 1:
                toolButton02Animator.SetBool("isMoving", true);
                break;
            case 2:
                toolButton03Animator.SetBool("isMoving", true);
                break;
            default:
                break;
        }
    }

    public void StopShowingSolution()
    {
        ActivatePuzzleButtons();
        UnHighlightAllPuzzleButtons();
        FocusOnButton(0);
    }

    public void ConvertCurrentSolutionToArray()
    {
        string solutionString = solutions[currentPuzzle];
        currentSolutionArray = new string[solutionString.Length];
        for (int i = 0; i < solutionString.Length; i++)
        {
            currentSolutionArray[i] = System.Convert.ToString(solutionString[i]);
        }
    }

    public void ActivatePuzzleButtons()
    {
        for (int i = 0; i < puzzleButtons.Length; i++)
        {
            puzzleButtons[i].interactable = true;
        }
    }

    public void DeactivatePuzzleButtons()
    {
        for (int i = 0; i < puzzleButtons.Length; i++)
        {
            puzzleButtons[i].interactable = false;
        }
    }

    public void ActivateStartQuitButton()
    {
        startButton.interactable = true;
        quitButton.interactable = true;
    }

    public void DeactivateStartQuitButton()
    {
        startButton.interactable = false;
        quitButton.interactable = false;
    }

    public void ResetAnimators()
    {
        toolButton01Animator.SetBool("isMoving", false);
        toolButton02Animator.SetBool("isMoving", false);
        toolButton03Animator.SetBool("isMoving", false);
    }

    public void ResetCurrentPuzzle()
    {
        solutionIndex = 0;
        currentAnswer = "";
        HighlightAllPuzzleButtons();
        DeactivatePuzzleButtons();
        ActivateStartQuitButton();
        FocusOnButton(3);
        resultText.enabled = false;
    }

    public void FocusOnButton(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                puzzleButtons[0].Select();
                puzzleButtons[0].OnSelect(null); 
                break;
            case 1:
                puzzleButtons[1].Select();
                puzzleButtons[1].OnSelect(null);
                break;
            case 2:
                puzzleButtons[2].Select();
                puzzleButtons[2].OnSelect(null);
                break;
            case 3:
                startButton.Select();
                startButton.OnSelect(null);
                break;
            case 4:
                quitButton.Select();
                quitButton.OnSelect(null);
                break;
            default:
                break;
        }
        
    }

    public void HighlightPuzzleButton(int toolButton)
    {
        switch (toolButton)
        {
            case 0:
                lockPickButtons[0].ChangeToHighlightedColor();
                break;
            case 1:
                lockPickButtons[1].ChangeToHighlightedColor();
                break;
            case 2:
                lockPickButtons[2].ChangeToHighlightedColor();
                break;
            default:
                break;
        }
    }

    public void HighlightAllPuzzleButtons()
    {
        for (int i = 0; i < lockPickButtons.Length; i++)
        {
            lockPickButtons[i].ChangeToHighlightedColor();
        }
    }

    public void UnHighlightAllPuzzleButtons()
    {
        for (int i = 0; i < lockPickButtons.Length; i++)
        {
            lockPickButtons[i].ChangeToNormalColor();
        }
    }

    public void PlayLockPickSFX()
    {
        audioSource.Play();
    }
}
