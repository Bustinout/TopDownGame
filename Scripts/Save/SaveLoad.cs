using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public static class SaveLoad
{
    public static Game current = new Game();

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/TopDownGame.save");
        bf.Serialize(file, SaveLoad.current);

        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/CaravanMaster.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/CaravanMaster.save", FileMode.Open);
            SaveLoad.current = (Game)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            NewGame();
        }
    }

    public static void NewGame()
    {
        SaveLoad.current = new Game();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/CaravanMaster.save");
        bf.Serialize(file, SaveLoad.current);
        file.Close();

        Save();


    }

}
