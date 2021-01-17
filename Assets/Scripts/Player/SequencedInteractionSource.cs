using UnityEngine;

public class SequencedInteractionSource: AbstractInteractionSource
{
    public int CurrentIndex;
    public AbstractOldInteraction[] Interactions;
    public Renderer[] HideOnCompletion;
    
    public override IInteraction GetInteraction(PlayerInteractionManager manager)
    {
        return CurrentIndex < Interactions.Length ? Interactions[CurrentIndex] : null;
    }

    public override void PerformInteraction(IInteraction interaction, PlayerInteractionManager manager)
    {
        CurrentIndex++;
        UpdateVisible();
    }

    private void UpdateVisible()
    {
        foreach (var renderer in HideOnCompletion)
        {
            renderer.enabled = CurrentIndex < Interactions.Length;
        }
    }
}