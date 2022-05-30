using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class BestScoreHandler : MonoBehaviour
{
    public static BestScoreHandler Instance;

    public TextMeshProUGUI nameInput;

    public int bestScore;
    public string bestPlayer;

    public string currentPlayer;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();

        // MenuUIHandler changes the currentPlayer based on the input field, but if starting on main scene, anon.
        currentPlayer = "anon";
    }


    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string bestPlayer;
    }

    // Save score is only called by the Main Manager when the current score is greater than the high score.
    public void SaveScore()
    {
        SaveData data = new SaveData();
        
        // the Main Manager updates the bestScore and bestPlayer to the current score and current player if the new score is higher than the best score
        data.bestScore = bestScore;
        data.bestPlayer = bestPlayer;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/DataPersistenceProject_savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/DataPersistenceProject_savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
            bestPlayer = data.bestPlayer;
        } else
        {
            bestPlayer = "none";
            bestScore = 0;
        }
    }
}
