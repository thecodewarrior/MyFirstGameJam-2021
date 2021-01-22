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

    protected int foodIndex;
    protected string wineButtonValue;
    protected string meatButtonValue;
    protected string riceButtonValue;

    protected int unitIndex;
    protected string tspButtonValue;
    protected string tblspButtonValue;
    protected string cupButtonValue;

    protected int quantityIndex;
    protected string fifteenButtonValue;
    protected string twentyButtonValue;
    protected string fiveButtonValue;

    [Header("Food Objects")]
    public PuzzleItemIcon[] foodItemIcons;
    public Image wineButtonImage;
    public Text wineButtonText;
    public Image meatButtonImage;
    public Text meatButtonText;
    public Image riceButtonImage;
    public Text riceButtonText;

    [Header("Unit Objects")]
    public PuzzleItemIcon[] unitItemIcons;
    public Image tspButtonImage;
    public Text tspButtonText;
    public Image tblspButtonImage;
    public Text tblspButtonText;
    public Image cupButtonImage;
    public Text cupButtonText;

    [Header("Quantity Objects")]
    public PuzzleItemIcon[] quantityItemIcons;
    public Image fifteenButtonImage;
    public Image twentyButtonImage;
    public Image fiveButtonImage;

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

    public void FoodPlayerInput(string buttonName)
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

    public void UnitPlayerInput(string buttonName)
    {
        switch (buttonName)
        {
            case "tspButton":
                CycleThroughUnit("tspButton");
                break;
            case "tblspButton":
                CycleThroughUnit("tblspButton");
                break;
            case "cupButton":
                CycleThroughUnit("cupButton");
                break;
            default:
                print("no button found.");
                break;
        }
    }

    public void QuantityPlayerInput(string buttonName)
    {
        switch (buttonName)
        {
            case "fifteenButton":
                CycleThroughQuantity("fifteenButton");
                break;
            case "twentyButton":
                CycleThroughQuantity("twentyButton");
                break;
            case "fiveButton":
                CycleThroughQuantity("fiveButton");
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

    public void CycleThroughUnit(string buttonName)
    {
        if (buttonName == "tspButton")
        {

            switch (unitIndex)
            {
                case 0:
                    unitIndex = 1;
                    CycleThroughUnit(buttonName);
                    break;
                case 1:
                    if (CheckUnitItemAlreadyUsed("tblsp"))
                    {
                        unitIndex = 2;
                        CycleThroughUnit(buttonName);
                        return;
                    }

                    tspButtonImage.sprite = unitItemIcons[1].iconSprite;
                    tspButtonValue = unitItemIcons[1].itemValue;
                    tspButtonText.text = unitItemIcons[1].itemName;

                    break;
                case 2:
                    if (CheckUnitItemAlreadyUsed("tsp"))
                    {
                        unitIndex = 3;
                        CycleThroughUnit(buttonName);
                        return;
                    }

                    tspButtonImage.sprite = unitItemIcons[2].iconSprite;
                    tspButtonValue = unitItemIcons[2].itemValue;
                    tspButtonText.text = unitItemIcons[2].itemName;
                    
                    break;
                case 3:
                    if (CheckUnitItemAlreadyUsed("cup"))
                    {
                        if (tspButtonValue == "" || tspButtonValue == null)
                        {
                            unitIndex = 1;
                            CycleThroughUnit(buttonName);
                            return;
                        }

                        unitIndex = 0;
                        tspButtonImage.sprite = unitItemIcons[0].iconSprite;
                        tspButtonValue = unitItemIcons[0].itemValue;
                        tspButtonText.text = unitItemIcons[0].itemValue;
                        return;
                    }
                    tspButtonImage.sprite = unitItemIcons[3].iconSprite;
                    tspButtonValue = unitItemIcons[3].itemValue;
                    tspButtonText.text = unitItemIcons[3].itemName;
                    break;
                default:
                    break;
            }
        }

        if (buttonName == "tblspButton")
        {

            switch (unitIndex)
            {
                case 0:
                    unitIndex = 1;
                    CycleThroughUnit(buttonName);
                    break;
                case 1:
                    if (CheckUnitItemAlreadyUsed("tblsp"))
                    {
                        unitIndex = 2;
                        CycleThroughUnit(buttonName);
                        return;
                    }

                    tblspButtonImage.sprite = unitItemIcons[1].iconSprite;
                    tblspButtonValue = unitItemIcons[1].itemValue;
                    tblspButtonText.text = unitItemIcons[1].itemName;

                    break;
                case 2:
                    if (CheckUnitItemAlreadyUsed("tsp"))
                    {
                        unitIndex = 3;
                        CycleThroughUnit(buttonName);
                        return;
                    }

                    tblspButtonImage.sprite = unitItemIcons[2].iconSprite;
                    tblspButtonValue = unitItemIcons[2].itemValue;
                    tblspButtonText.text = unitItemIcons[2].itemName;
                    
                    break;
                case 3:
                    if (CheckUnitItemAlreadyUsed("cup"))
                    {
                        if (tblspButtonValue == "" || tblspButtonValue == null)
                        {
                            unitIndex = 1;
                            CycleThroughUnit(buttonName);
                            return;
                        }

                        unitIndex = 0;
                        tblspButtonImage.sprite = unitItemIcons[0].iconSprite;
                        tblspButtonValue = unitItemIcons[0].itemValue;
                        tblspButtonText.text = unitItemIcons[0].itemValue;
                        return;
                    }
                    tblspButtonImage.sprite = unitItemIcons[3].iconSprite;
                    tblspButtonValue = unitItemIcons[3].itemValue;
                    tblspButtonText.text = unitItemIcons[3].itemName;
                    break;
                default:
                    break;
            }
        }

        if (buttonName == "cupButton")
        {

            switch (unitIndex)
            {
                case 0:
                    unitIndex = 1;
                    CycleThroughUnit(buttonName);
                    break;
                case 1:
                    if (CheckUnitItemAlreadyUsed("tsp"))
                    {
                        unitIndex = 2;
                        CycleThroughUnit(buttonName);
                        return;
                    }

                    cupButtonImage.sprite = unitItemIcons[1].iconSprite;
                    cupButtonValue = unitItemIcons[1].itemValue;
                    cupButtonText.text = unitItemIcons[1].itemName;

                    break;
                case 2:
                    if (CheckUnitItemAlreadyUsed("tblsp"))
                    {
                        unitIndex = 3;
                        CycleThroughUnit(buttonName);
                        return;
                    }

                    cupButtonImage.sprite = unitItemIcons[2].iconSprite;
                    cupButtonValue = unitItemIcons[2].itemValue;
                    cupButtonText.text = unitItemIcons[2].itemName;
                    break;
                case 3:
                    if (CheckUnitItemAlreadyUsed("cup"))
                    {
                        if (cupButtonValue == "" || cupButtonValue == null)
                        {
                            unitIndex = 1;
                            CycleThroughUnit(buttonName);
                            return;
                        }

                        unitIndex = 0;
                        cupButtonImage.sprite = unitItemIcons[0].iconSprite;
                        cupButtonValue = unitItemIcons[0].itemValue;
                        cupButtonText.text = unitItemIcons[0].itemValue;
                        return;
                    }
                    cupButtonImage.sprite = unitItemIcons[3].iconSprite;
                    cupButtonValue = unitItemIcons[3].itemValue;
                    cupButtonText.text = unitItemIcons[3].itemName;
                    break;
                default:
                    break;
            }
        }
    }

    public void CycleThroughQuantity(string buttonName)
    {
        if (buttonName == "fifteenButton")
        {

            switch (quantityIndex)
            {
                case 0:
                    quantityIndex = 1;
                    CycleThroughQuantity(buttonName);
                    break;
                case 1:
                    if (CheckQuantityItemAlreadyUsed("twenty"))
                    {
                        quantityIndex = 2;
                        CycleThroughQuantity(buttonName);
                        return;
                    }

                    fifteenButtonImage.sprite = quantityItemIcons[1].iconSprite;
                    fifteenButtonValue = quantityItemIcons[1].itemValue;

                    break;
                case 2:
                    if (CheckQuantityItemAlreadyUsed("fifteen"))
                    {
                        quantityIndex = 3;
                        CycleThroughQuantity(buttonName);
                        return;
                    }

                    fifteenButtonImage.sprite = quantityItemIcons[2].iconSprite;
                    fifteenButtonValue = quantityItemIcons[2].itemValue;
                    
                    break;
                case 3:
                    if (CheckQuantityItemAlreadyUsed("five"))
                    {
                        if (fifteenButtonValue == "" || fifteenButtonValue == null)
                        {
                            quantityIndex = 1;
                            CycleThroughQuantity(buttonName);
                            return;
                        }

                        quantityIndex = 0;
                        fifteenButtonImage.sprite = quantityItemIcons[0].iconSprite;
                        fifteenButtonValue = quantityItemIcons[0].itemValue;

                        return;
                    }
                    fifteenButtonImage.sprite = quantityItemIcons[3].iconSprite;
                    fifteenButtonValue = quantityItemIcons[3].itemValue;

                    break;
                default:
                    break;
            }
        }

        if (buttonName == "twentyButton")
        {

            switch (quantityIndex)
            {
                case 0:
                    quantityIndex = 1;
                    CycleThroughQuantity(buttonName);
                    break;
                case 1:
                    if (CheckQuantityItemAlreadyUsed("twenty"))
                    {
                        quantityIndex = 2;
                        CycleThroughQuantity(buttonName);
                        return;
                    }

                    twentyButtonImage.sprite = quantityItemIcons[1].iconSprite;
                    twentyButtonValue = quantityItemIcons[1].itemValue;

                    break;
                case 2:
                    if (CheckQuantityItemAlreadyUsed("fifteen"))
                    {
                        quantityIndex = 3;
                        CycleThroughQuantity(buttonName);
                        return;
                    }

                    twentyButtonImage.sprite = quantityItemIcons[2].iconSprite;
                    twentyButtonValue = quantityItemIcons[2].itemValue;

                    break;
                case 3:
                    if (CheckQuantityItemAlreadyUsed("five"))
                    {
                        if (twentyButtonValue == "" || twentyButtonValue == null)
                        {
                            quantityIndex = 1;
                            CycleThroughQuantity(buttonName);
                            return;
                        }

                        quantityIndex = 0;
                        twentyButtonImage.sprite = quantityItemIcons[0].iconSprite;
                        twentyButtonValue = quantityItemIcons[0].itemValue;

                        return;
                    }
                    twentyButtonImage.sprite = quantityItemIcons[3].iconSprite;
                    twentyButtonValue = quantityItemIcons[3].itemValue;

                    break;
                default:
                    break;
            }
        }

        if (buttonName == "fiveButton")
        {

            switch (quantityIndex)
            {
                case 0:
                    quantityIndex = 1;
                    CycleThroughQuantity(buttonName);
                    break;
                case 1:
                    if (CheckQuantityItemAlreadyUsed("twenty"))
                    {
                        quantityIndex = 2;
                        CycleThroughQuantity(buttonName);
                        return;
                    }

                    fiveButtonImage.sprite = quantityItemIcons[1].iconSprite;
                    fiveButtonValue = quantityItemIcons[1].itemValue;

                    break;
                case 2:
                    if (CheckQuantityItemAlreadyUsed("fifteen"))
                    {
                        quantityIndex = 3;
                        CycleThroughQuantity(buttonName);
                        return;
                    }

                    fiveButtonImage.sprite = quantityItemIcons[2].iconSprite;
                    fiveButtonValue = quantityItemIcons[2].itemValue;
                    
                    break;
                case 3:
                    if (CheckQuantityItemAlreadyUsed("five"))
                    {
                        if (fiveButtonValue == "" || fiveButtonValue == null)
                        {
                            quantityIndex = 1;
                            CycleThroughQuantity(buttonName);
                            return;
                        }

                        quantityIndex = 0;
                        fiveButtonImage.sprite = quantityItemIcons[0].iconSprite;
                        fiveButtonValue = quantityItemIcons[0].itemValue;

                        return;
                    }
                    fiveButtonImage.sprite = quantityItemIcons[3].iconSprite;
                    fiveButtonValue = quantityItemIcons[3].itemValue;

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

    public bool CheckUnitItemAlreadyUsed(string itemName)
    {
        switch (itemName)
        {
            case "tsp":
                if (tspButtonValue == "tsp" || tblspButtonValue == "tsp" || cupButtonValue == "tsp")
                {
                    return true;
                }
                break;
            case "tblsp":
                if (tspButtonValue == "tblsp" || tblspButtonValue == "tblsp" || cupButtonValue == "tblsp")
                {
                    return true;
                }
                break;
            case "cup":
                if (tspButtonValue == "cup" || tblspButtonValue == "cup" || cupButtonValue == "cup")
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    public bool CheckQuantityItemAlreadyUsed(string itemName)
    {
        switch (itemName)
        {
            case "fifteen":
                if (fifteenButtonValue == "fifteen" || twentyButtonValue == "fifteen" || fiveButtonValue == "fifteen")
                {
                    return true;
                }
                break;
            case "twenty":
                if (fifteenButtonValue == "twenty" || twentyButtonValue == "twenty" || fiveButtonValue == "twenty")
                {
                    return true;
                }
                break;
            case "five":
                if (fifteenButtonValue == "five" || twentyButtonValue == "five" || fiveButtonValue == "five")
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
        {
            if (wineButtonValue == "wine" && meatButtonValue == "meat" && riceButtonValue == "rice"
                && tspButtonValue == "tsp" && tblspButtonValue == "tblsp" && cupButtonValue == "cup"
                && fifteenButtonValue == "fifteen" && twentyButtonValue == "twenty" && fiveButtonValue == "five")
            {
                print("you win");
            }
            else
            {
                print("wrong potion");
            }
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

        tspButtonValue = unitItemIcons[0].itemValue;
        tspButtonImage.sprite = unitItemIcons[0].iconSprite;
        tspButtonText.text = unitItemIcons[0].itemValue;
        tblspButtonValue = unitItemIcons[0].itemValue;
        tblspButtonImage.sprite = unitItemIcons[0].iconSprite;
        tblspButtonText.text = unitItemIcons[0].itemValue;
        cupButtonValue = unitItemIcons[0].itemValue;
        cupButtonImage.sprite = unitItemIcons[0].iconSprite;
        cupButtonText.text = unitItemIcons[0].itemValue;

        fifteenButtonValue = quantityItemIcons[0].itemValue;
        fifteenButtonImage.sprite = quantityItemIcons[0].iconSprite;
        twentyButtonValue = quantityItemIcons[0].itemValue;
        twentyButtonImage.sprite = quantityItemIcons[0].iconSprite;
        fiveButtonValue = quantityItemIcons[0].itemValue;
        fiveButtonImage.sprite = quantityItemIcons[0].iconSprite;
    }
}
