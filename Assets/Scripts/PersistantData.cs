using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistantData : MonoBehaviour
{
    public static PersistantData instance;
    public GameData data = new GameData();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null)
            Destroy(this);

        DestroySave();
        Load();
    }

    public void Save()
    {
        BinaryFormatter bd = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game.dat");

        bd.Serialize(file, data);
        file.Close();
    }

    public bool Load()
    {
        if (File.Exists(Application.persistentDataPath + "/game.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/game.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);
            file.Close();

            HuntData.ForceUpdateId(data.Hunts);
            LocalizationManager.Instance.currentLanguageID = data.Lang;

            return true;
        }

        return false;
    }

    public void DestroySave()
    {
        BinaryFormatter bd = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game.dat");

        bd.Serialize(file, new GameData());
        file.Close();
    }
}

[Serializable]
public class GameData
{
    public List<HuntData> Hunts = new List<HuntData>();
    public int HuntActive = -1;

    public LANG Lang = LANG.FR;

    public enum LANG
    {
        EN = 0,
        FR = 1,
    }

    public HuntData GetActiveHunt()
    {
        return Hunts.Find(hunt => hunt.id == HuntActive);
    }
}

[Serializable]
public class HuntData
{
    public int id = 0;
    public int pokemonNumber = -1;
    public int totalCount = 0;
    public bool Done = false;
    public bool Paused = false;
    public bool ShinyCharm = false;
    public DataPkm.GameVersion Game = DataPkm.GameVersion.UNKNOW;
    public DataPkm.HuntingMode Method = DataPkm.HuntingMode.UNKNOW;
    public float prob = 1f / 8192f;
    public string Name = "No name";

    private static List<int> _ids = new List<int>();

    public static void ForceUpdateId(List<HuntData> l)
    {
        foreach (HuntData d in l)
            _ids.Add(d.id);
    }

    public HuntData() {
        while (_ids.Contains(id))
            id++;
        _ids.Add(id);
    }

    ~HuntData() { _ids.Remove(id); }
}