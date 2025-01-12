using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int numberOfShownScores = 10;
    [SerializeField] private GameObject scoreUIPrefab;

    [Header("To be set per Scene")]
    [SerializeField] private GameObject Timer;
    [SerializeField] private GameObject highscoreUI;
    [SerializeField] private GameObject playerscoreUI;


    private string nameOfNewPlayer = "new pirate";
    private TextMeshProUGUI playerName;
    private GameObject playerInHighscoreUI;
    public GameObject GameTarget;
    private int timeInSeconds;
    public TMP_InputField inputField; // InputField, um Text einzugeben
    private List<GameObject> listOfUIScores = new List<GameObject>();
    private List<HighscoreElement> highscorelist = new List<HighscoreElement>();

    //private void Awake()
    //{
    // ////Zum Füllen der Highscoreliste
    // for (int i = 0; i < 15; i++)
    // {
    //     int random = Random.Range(0, 100);
    //     HighscoreElement newEntry = new HighscoreElement
    //     {
    //         score = random,
    //         name = "Player" + i
    //     };
    //     highscorelist.Add(newEntry);
    // }
    //}

    private void Update()
    {
        //Eigentlich unnötig in einem update gelöst, aber sollte gehen
        if (!GameTarget.GetComponent<FinishGame>().levelfinished) return;

        // Hole den Text aus dem InputField
        string input = inputField.text;

        if (playerInHighscoreUI != null)
        {
            playerName = playerInHighscoreUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            if (!string.IsNullOrEmpty(input))
            {
                //setze Namen
                playerName.text = input;

            }
            else
            {
                playerName.text = nameOfNewPlayer;
            }
        }
    }

    public void SetScoreUI()
    {
        BuildUI();

        LoadHighscoreList();

        SetNewScore();

        ShowPlayerScore();

        SetHighscoreUI();
    }

    public void SaveHighscoreList()
    {
        //Hinterlegen des geänderten Namens (Erst mit speichern, davor egal)
        for (int i = 0; i < highscorelist.Count; i++)
        {
            if (highscorelist[i].score == timeInSeconds)
            {
                highscorelist[i].name = playerName.text;
                //Stellt sicher, dass nicht als neuer Player in Datenbank
                highscorelist[i].isNewPlayer = false;
            }
        }

        //Build json
        Highscores highscores = new Highscores { highscoreList = highscorelist };
        string json = JsonUtility.ToJson(highscores);

        //Save String
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
    }

    private void BuildUI()
    {
        //Add Score
        Transform parent = highscoreUI.transform.GetChild(2);

        float templateHeight = 40f;
        listOfUIScores.Clear();

        for (int i = 0; i < numberOfShownScores; i++)
        {
            GameObject newScoreElement = Instantiate(scoreUIPrefab, parent);
            RectTransform newRectTransform = newScoreElement.GetComponent<RectTransform>();
            newRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);

            listOfUIScores.Add(newScoreElement);
        }
    }

    private void LoadHighscoreList()
    {
        //load highscorelist
        //PlayerPrefs.GetString("TEST");
    }


    private void SetNewScore()
    {
        //Add score
        timeInSeconds = Timer.transform.GetChild(1).GetComponent<Timer>().timeInSeconds;

        //Add default playername
        string input = nameOfNewPlayer;

        HighscoreElement newEntry = new HighscoreElement
        {
            score = timeInSeconds,
            name = input,
            isNewPlayer = true
        };

        //Add score-element to highscoreList
        highscorelist.Add(newEntry);
    }

    private void ShowPlayerScore()
    {
        //Show individuals result
        TextMeshProUGUI playerText = playerscoreUI.GetComponent<TextMeshProUGUI>();

        // Berechne Minuten und Sekunden
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // Formatiere die Zeit im Format "M:SS"und setzt den IndividualScore
        playerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void SetHighscoreUI()
    {
        // Sortiere die Liste nach Score in aufsteigender Reihenfolge
        highscorelist.Sort((a, b) => a.score.CompareTo(b.score));

        //Set Elements in UI
        for (int i = 0; i < numberOfShownScores; i++)
        {
            if (i <= highscorelist.Count - 1)
            {
                if (highscorelist[i].isNewPlayer)
                {
                    playerInHighscoreUI = listOfUIScores[i];
                }
                else
                {
                    //setze Name aus Highscoreliste
                    TextMeshProUGUI scoreName = listOfUIScores[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                    scoreName.text = highscorelist[i].name;
                }

                //Setze Score
                TextMeshProUGUI scoreTime = listOfUIScores[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                int singleScore = highscorelist[i].score;
                // Berechne Minuten und Sekunden
                int minutes = Mathf.FloorToInt(singleScore / 60);
                int seconds = Mathf.FloorToInt(singleScore % 60);
                // Formatiere die Zeit im Format "M:SS"und setzt den IndividualScore
                scoreTime.text = string.Format("{0}:{1:00}", minutes, seconds);


            }
            //Falls noch nicht genügend Ergebniss in der Liste sind
            else
            {
                //setze Namen
                TextMeshProUGUI scoreName = listOfUIScores[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                scoreName.text = "---";

                //Setze Score
                TextMeshProUGUI scoreTime = listOfUIScores[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                scoreTime.text = "---";
            }
        }
    }

    //Liste mit diversen Highscores fürs Speichern
    private class Highscores
    {
        public List<HighscoreElement> highscoreList;
    }

    //Single Score Element
    [System.Serializable]
    private class HighscoreElement
    {
        public int score;
        public string name;
        public bool isNewPlayer;
    }
}
