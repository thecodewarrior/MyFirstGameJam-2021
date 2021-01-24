using System.Xml;

public interface IPersistentObject
{
    string SaveID { get; }
    bool IsActive { get; }
    /**
     * <summary>Load from the given state.</summary>
     * <param name="saveState">The state to load from, or null if there is no state to load from</param>
     */
    void LoadSaveState(AbstractSaveState saveState);
    /**
     * <summary>Create a new save state object for serialization</summary>
     */
    AbstractSaveState GetSaveState();
}