using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Words")]
public class Words : ScriptableObject
{

    [SerializeField]
    [Header("It's text for worlds")]
    private TextAsset text;
    [SerializeField]
    [Header("Minimal count symbols in word")]
    private int minLength;
    [SerializeField]
    [Header("Count of your trying")]
    private int countTrying;
    [SerializeField]
    [Header("Use not rare word")]
    private bool useFewWord;
    [SerializeField]
    [Header("Use rare word")]
    private bool useLotWord;

    private List<QuantityPerUnit> wordOnRare; //слова по встречаемости

    public void CreateList()
    {
        wordOnRare = new List<QuantityPerUnit>();
        WordsFromText(text.text);
        Game.MaxCountTrying = countTrying;
    }

    void WordsFromText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            while (i < text.Length && !char.IsLetter(text[i]))
            {
                i++;
                continue;
            }
            char[] word = new char[0];
            while (i < text.Length && char.IsLetter(text[i]))
            {
                System.Array.Resize(ref word, word.Length + 1);
                word[word.Length - 1] = text[i];
                i++;
            }

            if (word.Length != 0 && word.Length >= minLength)
            {
                string val = new string(word).ToUpper();
                bool check = false;
                for (int j = 0; j < wordOnRare.Count; j++)
                {
                    if (wordOnRare[j].Word == val)
                    {
                        wordOnRare[j].Quantity++;
                        check = true;
                        break;
                    }
                }
                if (!check) wordOnRare.Add(new QuantityPerUnit(val));
            }
        }

        wordOnRare = wordOnRare.OrderBy(x => x.Quantity).ToList();

    }


    public string ChooseWord()
    {
        if (wordOnRare.Count() == 0)
        {
            Game.Instance.Win();
            return null;
        }

        string word = "";

        int number;

        if (useFewWord)
        {
            number = Random.Range(wordOnRare.Count() - 3, wordOnRare.Count());
            if (number < 0) number = 0;
        }
        else if (useLotWord)
        {
            number = Random.Range(0, 3);
            if (number >= wordOnRare.Count()) number = wordOnRare.Count() - 1;

        }
        else
            number = Random.Range(0, wordOnRare.Count());


        word = wordOnRare[number].Word;
        Debug.Log(wordOnRare[number].Word + "  Количество в тексте: " + wordOnRare[number].Quantity);
        wordOnRare[number].Quantity--;
        if (wordOnRare[number].Quantity <= 0)
            wordOnRare.RemoveAt(number);

        return word;
    }

}

[System.Serializable]
public class QuantityPerUnit
{
    public string Word { get; set; }
    public int Quantity { get; set; }//Частота встречаемости

    public QuantityPerUnit(string Word)
    {
        this.Word = Word;
        Quantity = 1;
    }
}