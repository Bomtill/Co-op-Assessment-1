using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    //Singleton
    static public SaveSystem instance;



    string filePath;
    string fileName; // could have the player set the file name for seperate saves
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        filePath = Application.persistentDataPath + "/save.data"; // + fileName;
        #region singleton
        if (instance == null)
        {
            instance = this;
        } else Destroy(gameObject);
        #endregion
    }

    public void SaveGame(GameData saveData)
    {
        FileStream dataStream = new FileStream(filePath, FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, saveData);

        dataStream.Close();
    }

    public GameData LoadGame()
    {
        if (File.Exists(filePath))
        {
            // if file exists, return it
            FileStream dataStream = new FileStream(filePath, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            GameData saveData = converter.Deserialize(dataStream) as GameData;

            dataStream.Close();
            return saveData;
        } 
        else
        {
            // if not make file dosen not exist
            Debug.LogError("Save file not found at" + filePath);
            return null;
        }
    }
}
