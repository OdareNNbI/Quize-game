using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Delegates
{
    public delegate void OnEnable();
}

public class Game : MonoBehaviour {

    public static event Delegates.OnEnable onEnable;

    [SerializeField]
    private Words text;
    [SerializeField]
    private Text countTryingText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private Text scoreInLOsePanel;
    
    private int countTrying;

    public static int MaxCountTrying { get; set; }
    private static int Score;

    public static Game Instance { get; set; }


    public int CountTrying
    {
        get
        {
            return countTrying;
        }
        set
        {
            countTrying = value;
            if (countTrying <= 0) Lose();
            countTryingText.text = "Count trying : " + countTrying;
        }
    }


    void Start()
    {
        if (Instance == null)
        {
            text.CreateList();
            countTrying = MaxCountTrying;
            losePanel.SetActive(false);
            scoreText.text = "Score: 0";
            countTryingText.text = "Count trying: " + countTrying.ToString();
            Instance = this;
            NewWord();
        }
    }
    



    public void AddScore()
    {
        Score += countTrying;
        countTrying = MaxCountTrying;
        onEnable();
        scoreText.text ="Score: " + Score.ToString();
        NewWord();
    }

    public void Lose()
    {
        losePanel.SetActive(true);
        scoreInLOsePanel.text = ("LOSE\nYour score : " + Score.ToString()).ToUpper();
    }

    public void Win()
    {
        losePanel.SetActive(true);
        scoreInLOsePanel.text = ("YOU WIN\nYOUR SCORE : " + Score.ToString()).ToUpper();
    }

    public void onMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void NewWord()
    {
        WordInGame.Instance.CreateWord(text.ChooseWord());
    }
}
