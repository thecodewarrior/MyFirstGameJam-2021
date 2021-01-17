namespace Interactions.Triggers
{
    public abstract class AbstractTrigger : InteractionNode
    {
        protected override bool EnterLate => true;
    }
}