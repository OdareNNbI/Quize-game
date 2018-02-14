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

    private List<string> words;
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
            words = text.words;
            WordInGame.Instance.CreateWord(GenerateWord(ref words));
        }
    }
    

    string GenerateWord(ref List<string> words)
    {   
        if(words.Count == 0)
        {
            Win();
            return null;
        }

        int number = Random.Range(0, words.Count);
        string word = words[number];
        print(word);
        words.RemoveAt(number);
        return word;
    }

    public void AddScore()
    {
        Score += countTrying;
        countTrying = MaxCountTrying;
        onEnable();
        scoreText.text ="Score: " + Score.ToString();
        WordInGame.Instance.CreateWord(GenerateWord(ref words));
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
}
