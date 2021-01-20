using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockPickButton : MonoBehaviour
{
    protected LockPickPuzzleController lockPickPuzzleController;
    public Button toolButton;
    public int toolNumber;
    public Color normalColor;
    public Color highlightedColor;
    public Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        lockPickPuzzleController = FindObjectOfType<LockPickPuzzleController>();
    }

    void Update()
    {
    }
    public void HandlePlayerInput()
    {
        lockPickPuzzleController.HandlePlayerInput(toolNumber);
    }

    public void ChangeToNormalColor()
    {
        buttonImage.color = normalColor;   
    }

    public void ChangeToHighlightedColor()
    {
        buttonImage.color = highlightedColor;    
    }
}
