using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordInGame : MonoBehaviour {

    [SerializeField]
    private Image letterObject;

    private Image[] letters;

    private int countOpenedLetter;
    private int countLetters;
    private char[] word;
    public static WordInGame Instance { get; set; }
    

    void Start() {
        if (Instance == null)
        {
            Instance = this;
        }

    }
	
    public void CreateWord(string word)
    {
        if (word != null)
        {
            countOpenedLetter = 0;

            this.word = word.ToCharArray();

            if (letters != null)
            {
                for (int i = 0; i < letters.Length; i++)
                    Destroy(letters[i].gameObject);
            }
            countLetters = word.Length;

            letters = new Image[countLetters];
            for (int i = 0; i < countLetters; i++)
            {
                letters[countLetters - i - 1] = Instantiate(letterObject, transform);
                letters[countLetters - i - 1].GetComponentInChildren<Text>().text = word[countLetters - i - 1].ToString();
                letters[countLetters - i - 1].GetComponentInChildren<Text>().enabled = false;
            }
        }
    }
    
    public void GetLetter(char letter)
    {
        int check = countOpenedLetter;
        for(int i =0;i < countLetters;i++)
        {
            if((word[i]) == letter)
            {
                countOpenedLetter++;
                letters[i].GetComponentInChildren<Text>().enabled = true;
                letters[i].enabled = false;

                if(countOpenedLetter == countLetters)
                {
                    Game.Instance.AddScore();
                    return;
                }
            }
        }

        if (check == countOpenedLetter)
            Game.Instance.CountTrying--;
    }
}
