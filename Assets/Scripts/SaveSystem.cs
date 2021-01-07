﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData (GameManager gameManager, AudioManager audioManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.jenga";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gameManager, audioManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData ()
    {
        string path = Application.persistentDataPath + "/game.jenga";


        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
