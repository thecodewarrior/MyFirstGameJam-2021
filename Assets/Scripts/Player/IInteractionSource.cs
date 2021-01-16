public interface IInteractionSource
{
    public IInteraction GetInteraction(PlayerInteractionManager manager);
    public void PerformInteraction(IInteraction interaction, PlayerInteractionManager manager);
}