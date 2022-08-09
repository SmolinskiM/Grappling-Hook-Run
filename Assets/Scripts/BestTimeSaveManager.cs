using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BestTimeSaveManager : MonoBehaviour
{
    public readonly BestTime bestTime = new BestTime();
   
    private static BestTimeSaveManager instance;
    private string savePath;

    public static BestTimeSaveManager Instance { get { return instance; } }

    private void Awake()
    {
        savePath = Application.dataPath + "/Save/bestTime.json";
        if (instance != this)
        {
            Destroy(instance);
        }

        instance = this;

        if(File.Exists(savePath))
        {
            LoadBestsTime();
        }
        else
        {
            bestTime.bestTime = new float[Resources.LoadAll<Level>("").Length];
        }
    }

    public void SaveBestTime(float bestTime, int level)
    {
        this.bestTime.bestTime[level] = bestTime;

        string jsonContent = JsonUtility.ToJson(this.bestTime, true);

        FileStream fileStream = new FileStream(savePath, FileMode.Create);

        using(StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(jsonContent);
        }
    }

    private void LoadBestsTime()
    {
        StreamReader reader = new StreamReader(savePath);

        string jsonContent = reader.ReadToEnd();

        BestTime bt = JsonUtility.FromJson<BestTime>(jsonContent);

        bestTime.bestTime = bt.bestTime;
    }
}
