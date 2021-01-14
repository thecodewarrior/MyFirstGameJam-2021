using System.Xml;

public interface ISavable
{
    string SaveID { get; }
    /**
     * All savable objects are reset before loading in case there was no save data to load from.
     */
    void ResetSaveState();
    /**
     * Write the save data to the save XML file
     */
    void WriteSaveState(XmlWriter writer);
    /**
     * Read the save data from the save XML file
     */
    void ReadSaveState(XmlReader reader);
}