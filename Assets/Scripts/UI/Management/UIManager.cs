using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static bool HasInputFocus { get; private set; }

    [SerializeField] private AbstractUIController ActiveController;
    private AbstractUIController ActiveDialogController;

    [SerializeField] private UIDocument MainDocument;
    [SerializeField] private UIDocument DialogDocument;

    private Stack<AbstractUIController> _controllerStack = new Stack<AbstractUIController>();

    private void Start()
    {
        if (ActiveController != null)
            Open(ActiveController);
    }

    public void Push(AbstractUIController controller)
    {
        _controllerStack.Push(ActiveController);
        Open(controller);
    }

    public void Pop()
    {
        if (_controllerStack.Count > 0)
            Open(_controllerStack.Pop());
        else
            Open(null);
    }

    public void Open(AbstractUIController controller)
    {
        if (ActiveController != null)
            ActiveController.Close();

        if (controller == null)
        {
            MainDocument.visualTreeAsset = null;
        }
        else
        {
            MainDocument.visualTreeAsset = controller.UITemplate;
            controller.Open(MainDocument.rootVisualElement);
        }

        ActiveController = controller;

        if (ActiveController != null && ActiveController.PausesTime)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void OpenDialog(AbstractUIController controller)
    {
        if (ActiveDialogController != null)
            ActiveDialogController.Close();

        if (controller == null)
        {
            DialogDocument.visualTreeAsset = null;
        }
        else
        {
            DialogDocument.visualTreeAsset = controller.UITemplate;
            controller.Open(DialogDocument.rootVisualElement);
        }

        ActiveDialogController = controller;
    }

    // run in late update so 
    private void LateUpdate()
    {
        HasInputFocus = ActiveController != null || ActiveDialogController != null;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}