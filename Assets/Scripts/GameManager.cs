using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int bestScore;
    public string playerName;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable] 
    public class SaveData
    {
        public int bestScore;
        public string playerName;
    }

    [System.Serializable]
    public class MyCollection
    {
        public List<SaveData> saveData = new List<SaveData>();
    }

    public void SaveBest()
    {
        MyCollection fullData = new MyCollection();
        string path;
        string json;

        path = Application.persistentDataPath + "/bestScore.json";

        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
            fullData = JsonUtility.FromJson<MyCollection>(json);

        }

        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.playerName = playerName;
        int index = 0;
        int indexForInsert = -1;

        foreach (SaveData fullDat in fullData.saveData)
        {
            if (fullDat.playerName == data.playerName)
            {
                Debug.Log("Find compare");
                if (fullDat.bestScore < data.bestScore && indexForInsert == -1)
                {
                    fullData.saveData[index].bestScore = data.bestScore;
                    json = JsonUtility.ToJson(fullData);

                    File.WriteAllText(Application.persistentDataPath + "/bestScore.json", json);
                    return;
                } else if (indexForInsert != -1)
                {
                    fullData.saveData.Remove(fullData.saveData[index]);
                    break;
                }
            }

            if (fullDat.bestScore < data.bestScore && indexForInsert == -1)
            {
                indexForInsert = index;
            }
            index++;
        }
        if (indexForInsert != -1) { fullData.saveData.Insert(indexForInsert, data); } else { fullData.saveData.Insert(index, data); };
        
        json = JsonUtility.ToJson(fullData);
        File.WriteAllText(Application.persistentDataPath + "/bestScore.json", json);

    }

    public void ReturnAllBest(TMP_Text text)
    {
        MyCollection fullData = new MyCollection();
        string path = Application.persistentDataPath + "/bestScore.json";
        string json;
        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
            fullData = JsonUtility.FromJson<MyCollection>(json);
        }
        text.text = "";
        int top10 = 0;
        foreach (SaveData fullDat in fullData.saveData)
        {
            text.text += $"{fullDat.playerName} BEST: {fullDat.bestScore} \n";
            top10++;
            if (top10 > 9) break;
        }
    }

    public string CheckBest()
    {
        MyCollection fullData = new MyCollection();
        string path = Application.persistentDataPath + "/bestScore.json";
        string json;

        bestScore = 0;

        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
            fullData = JsonUtility.FromJson<MyCollection>(json);
        }
        else
        {
            return "0";
        }

        foreach (SaveData fullDat in fullData.saveData)
        {
            if (fullDat.playerName == playerName)
            {
                bestScore = fullDat.bestScore;
            }
        }
        return bestScore.ToString();
    }
}
