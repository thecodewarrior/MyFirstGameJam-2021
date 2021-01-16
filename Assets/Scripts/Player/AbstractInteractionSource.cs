using UnityEngine;

public abstract class AbstractInteractionSource : MonoBehaviour, IInteractionSource
{
    public abstract IInteraction GetInteraction(PlayerInteractionManager manager);
    public abstract void PerformInteraction(IInteraction interaction, PlayerInteractionManager manager);

    private void OnTriggerEnter2D(Collider2D other)
    {
        var manager = other.GetComponent<PlayerInteractionManager>();
        if (manager)
        {
            manager.AddSource(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var manager = other.GetComponent<PlayerInteractionManager>();
        if (manager)
        {
            manager.RemoveSource(this);
        }
    }
}