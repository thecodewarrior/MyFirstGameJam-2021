namespace Interactions.Actions
{
    public class LockPickPuzzleAction : AbstractAction
    {
        public InteractionNode Fail;
        public InteractionNode Success;

        protected override void OnEnterNode()
        {
            AdvanceTo(Success);
        }
    }
}