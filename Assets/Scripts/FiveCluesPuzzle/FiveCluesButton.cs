using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiveCluesButton : MonoBehaviour
{

    protected FiveCluesPuzzleController fiveCluesPuzzleController;

    public string buttonName;

    // Start is called before the first frame update
    void Start()
    {
        fiveCluesPuzzleController = FindObjectOfType<FiveCluesPuzzleController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FoodPlayerInput()
    {
        fiveCluesPuzzleController.FoodPlayerInput(buttonName);
    }

    public void UnitPlayerInput()
    {
        fiveCluesPuzzleController.UnitPlayerInput(buttonName);
    }

    public void QuantityPlayerInput()
    {
        fiveCluesPuzzleController.QuantityPlayerInput(buttonName);
    }
}
