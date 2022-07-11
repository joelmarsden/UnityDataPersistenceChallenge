using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance;

    private int m_HighScore = 0;
    private string m_HighScorePlayer;

    private string currentPlayer;

    public string getBestScoreText()
    {
        string txt = "Best Score : " + getHighScorePlayer() + " : ";
        if (getHighScore() > 0)
        {
            txt += getHighScore();
        }
        return txt;
    }

    public int getHighScore()
    {
        return m_HighScore;
    }

    public string getHighScorePlayer()
    {
        return m_HighScorePlayer;
    }

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    public string getCurrentPlayer()
    {
        return currentPlayer;
    }

    public void setCurrentPlayer(string player)
    {
        currentPlayer = player;
    }

    public void updateHighScore(int highScore)
    {
        if( highScore > m_HighScore )
        {
            m_HighScore = highScore;
            m_HighScorePlayer = currentPlayer;
            SaveHighScore();
        }
    }


    [System.Serializable]
    class SaveData
    {
        public string HighScorePlayer;
        public int HighScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.HighScorePlayer = currentPlayer;
        data.HighScore = m_HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {

        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log("load path: " + path);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            this.m_HighScorePlayer = data.HighScorePlayer;
            this.m_HighScore = data.HighScore;
        }
    }

}
