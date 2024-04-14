using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CreateSaveScript
{
    public static void Save(ShowPlayerScript player, UpLevelPlayerScript level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Player.text";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameSaveScript gameSave = new GameSaveScript(player, level);

        formatter.Serialize(stream, gameSave);
        stream.Close();
    }

    public static GameSaveScript LoadGame()
    {
        string path = Application.persistentDataPath + "/Player.text";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameSaveScript gameSave = formatter.Deserialize(stream) as GameSaveScript;
            stream.Close();
            return gameSave;
        }
        else
        {
            Debug.LogError("not found Save in" + path);
            return null;
        }
    }
}
