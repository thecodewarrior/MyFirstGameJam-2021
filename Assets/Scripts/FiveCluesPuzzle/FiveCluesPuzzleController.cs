using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PuzzleItemIcon
{
    public Sprite iconSprite;
    public string itemName;
    public string itemValue;
}

public class FiveCluesPuzzleController : MonoBehaviour
{

    public int foodIndex;
    public string wineButtonValue;
    public string meatButtonValue;
    public string riceButtonValue;


    public PuzzleItemIcon[] foodItemIcons;
    public Image wineButtonImage;
    public Text wineButtonText;
    public Image meatButtonImage;
    public Text meatButtonText;
    public Image riceButtonImage;
    public Text riceButtonText;

    public Button tspButton;
    public Button tblspButton;
    public Button cupButton;
    public Button fifteenButton;
    public Button twentyButton;
    public Button fiveButton;

    void OnEnable()
    {
        ResetPuzzle();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoPlayerInput(string buttonName)
    {
        switch (buttonName)
        {
            case "wineButton":
                CycleThroughFood("wineButton");
                break;
            case "meatButton":
                CycleThroughFood("meatButton");
                break;
            case "riceButton":
                CycleThroughFood("riceButton");
                break;
            default:
                print("no button found.");
                break;
        }
    }

    public void CycleThroughFood(string buttonName)
    {
        if(buttonName == "wineButton")
        {

            switch (foodIndex)
            {
                case 0:
                    foodIndex = 1;
                    CycleThroughFood(buttonName);
                    break;
                case 1:
                    if (CheckFoodItemAlreadyUsed("wine"))
                    {
                        foodIndex = 2;
                        CycleThroughFood(buttonName);
                        return;
                    }

                    wineButtonImage.sprite = foodItemIcons[1].iconSprite;
                    wineButtonValue = foodItemIcons[1].itemValue;
                    wineButtonText.text = foodItemIcons[1].itemName;
                    
                    break;
                case 2:
                    if (CheckFoodItemAlreadyUsed("meat"))
                    {
                        foodIndex = 3;
                        CycleThroughFood(buttonName);
                        return;
                    }

                    wineButtonImage.sprite = foodItemIcons[2].iconSprite;
                    wineButtonValue = foodItemIcons[2].itemValue;
                    wineButtonText.text = foodItemIcons[2].itemName;
                    break;
                case 3:
                    if (CheckFoodItemAlreadyUsed("rice"))
                    {
                        if (wineButtonValue == "" || wineButtonValue == null)
                        {
                            foodIndex = 1;
                            CycleThroughFood(buttonName);
                            return;
                        }

                        foodIndex = 0;
                        wineButtonImage.sprite = foodItemIcons[0].iconSprite;
                        wineButtonValue = foodItemIcons[0].itemValue;
                        wineButtonText.text = foodItemIcons[0].itemValue;
                        return;
                    }
                    wineButtonImage.sprite = foodItemIcons[3].iconSprite;
                    wineButtonValue = foodItemIcons[3].itemValue;
                    wineButtonText.text = foodItemIcons[3].itemName;
                    break;
                default:
                    break;
            }
        }

        if (buttonName == "meatButton")
        {
            switch (foodIndex)
            {
                case 0:
                    foodIndex = 1;
                    CycleThroughFood(buttonName);
                    break;
                case 1:
                    if (CheckFoodItemAlreadyUsed("wine"))
                    {
                        foodIndex = 2;
                        CycleThroughFood(buttonName);
                        return;
                    }

                    meatButtonImage.sprite = foodItemIcons[1].iconSprite;
                    meatButtonValue = foodItemIcons[1].itemValue;
                    meatButtonText.text = foodItemIcons[1].itemName;

                    break;
                case 2:
                    if (CheckFoodItemAlreadyUsed("meat"))
                    {
                        foodIndex = 3;
                        CycleThroughFood(buttonName);
                        return;
                    }

                    meatButtonImage.sprite = foodItemIcons[2].iconSprite;
                    meatButtonValue = foodItemIcons[2].itemValue;
                    meatButtonText.text = foodItemIcons[2].itemName;
                    break;
                case 3:
                    if (CheckFoodItemAlreadyUsed("rice"))
                    {
                        if (meatButtonValue == "" || meatButtonValue == null)
                        {
                            foodIndex = 1;
                            CycleThroughFood(buttonName);
                            return;
                        }
                        foodIndex = 0;
                        meatButtonImage.sprite = foodItemIcons[0].iconSprite;
                        meatButtonValue = foodItemIcons[0].itemValue;
                        meatButtonText.text = foodItemIcons[0].itemValue;
                        return;
                    }
                    meatButtonImage.sprite = foodItemIcons[3].iconSprite;
                    meatButtonValue = foodItemIcons[3].itemValue;
                    meatButtonText.text = foodItemIcons[3].itemName;
                    break;
                default:
                    break;
            }
        }

        if (buttonName == "riceButton")
        {
            switch (foodIndex)
            {
                case 0:
                    foodIndex = 1;
                    CycleThroughFood(buttonName);
                    break;
                case 1:
                    if (CheckFoodItemAlreadyUsed("wine"))
                    {
                        foodIndex = 2;
                        CycleThroughFood(buttonName);
                        return;
                    }

                    riceButtonImage.sprite = foodItemIcons[1].iconSprite;
                    riceButtonValue = foodItemIcons[1].itemValue;
                    riceButtonText.text = foodItemIcons[1].itemName;

                    break;
                case 2:
                    if (CheckFoodItemAlreadyUsed("meat"))
                    {
                        foodIndex = 3;
                        CycleThroughFood(buttonName);
                        return;
                    }

                    riceButtonImage.sprite = foodItemIcons[2].iconSprite;
                    riceButtonValue = foodItemIcons[2].itemValue;
                    riceButtonText.text = foodItemIcons[2].itemName;
                    break;
                case 3:
                    if (CheckFoodItemAlreadyUsed("rice"))
                    {
                        if (riceButtonValue == "" || riceButtonValue == null)
                        {
                            foodIndex = 1;
                            CycleThroughFood(buttonName);
                            return;
                        }

                        foodIndex = 0;
                        riceButtonImage.sprite = foodItemIcons[0].iconSprite;
                        riceButtonValue = foodItemIcons[0].itemValue;
                        riceButtonText.text = foodItemIcons[0].itemValue;
                        return;
                    }
                    riceButtonImage.sprite = foodItemIcons[3].iconSprite;
                    riceButtonValue = foodItemIcons[3].itemValue;
                    riceButtonText.text = foodItemIcons[3].itemName;
                    break;
                default:
                    break;
            }
        }
    }

    public bool CheckFoodItemAlreadyUsed(string itemName)
    {
        switch (itemName)
        {
            case "wine":
                if(wineButtonValue == "wine" || meatButtonValue == "wine" || riceButtonValue == "wine")
                {
                    return true;
                }
                break;
            case "meat":
                if (wineButtonValue == "meat" || meatButtonValue == "meat" || riceButtonValue == "meat")
                {
                    return true;
                }
                break;
            case "rice":
                if (wineButtonValue == "rice" || meatButtonValue == "rice" || riceButtonValue == "rice")
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    public void SubmitPotion()
    {
        if(wineButtonValue == "wine" && meatButtonValue == "meat" && riceButtonValue == "rice")
        {
            print("you win");
        } else
        {
            print("wrong potion");
        }
    }

    public void ResetPuzzle()
    {
        wineButtonValue = foodItemIcons[0].itemValue;
        wineButtonImage.sprite = foodItemIcons[0].iconSprite;
        wineButtonText.text = foodItemIcons[0].itemValue;
        meatButtonValue = foodItemIcons[0].itemValue;
        meatButtonImage.sprite = foodItemIcons[0].iconSprite;
        meatButtonText.text = foodItemIcons[0].itemValue;
        riceButtonValue = foodItemIcons[0].itemValue;
        riceButtonImage.sprite = foodItemIcons[0].iconSprite;
        riceButtonText.text = foodItemIcons[0].itemValue;
    }
}
