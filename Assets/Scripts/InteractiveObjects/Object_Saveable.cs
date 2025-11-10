using UnityEditor;
using UnityEngine;

public class Object_Saveable : MonoBehaviour, ISaveable
{
    public void LoadData(GameData data)
    {
        Object_Chest[] allChests = FindObjectsByType<Object_Chest>(FindObjectsSortMode.None);
        foreach (var savedChests in data.chests)
        {
            string saveID = savedChests.Key;
            bool open = savedChests.Value;
            foreach (Object_Chest chest in allChests)
            {
                if (chest.GetID() == saveID)
                    chest.SetStateFromSave(open);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.chests.Clear();
        Object_Chest[] allChests = FindObjectsByType<Object_Chest>(FindObjectsSortMode.None);
        foreach (var chest in allChests)
        {
            string chestID = chest.GetID();
            bool open = chest.ChestOpen();
            data.chests[chestID] = open;
        }
    }
}