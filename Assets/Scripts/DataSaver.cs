using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static DataSaver Instance;

    public int HighScore;
    public string HighScorer;
    public string CurrentPlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string HighScorer;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.HighScore = HighScore;
        data.HighScorer = HighScorer;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.HighScore;
            HighScorer = data.HighScorer;
        }
    }

    public void TakeName(string name)
    {
        CurrentPlayer = name;
    }
}
