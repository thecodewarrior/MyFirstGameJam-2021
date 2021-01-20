namespace Interactions
{
    public class SaveNode : InteractionNode
    {
        public string ID;
        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            if(SaveManager != null)
                SaveManager.MarkSavePoint(this);
            AdvanceTo(Next);
        }
    }
}