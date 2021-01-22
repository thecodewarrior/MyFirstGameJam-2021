using UnityEngine;

namespace Interactions
{
    public abstract class AbstractPuzzleController : MonoBehaviour
    {
        public Canvas Canvas;
        
        public delegate void CompletionCallback(bool wasSuccessful);
        public CompletionCallback OnCompletion;

        public void Show()
        {
            Canvas.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            Canvas.gameObject.SetActive(false);
        }

        public abstract void ResetPuzzle();
    }
}