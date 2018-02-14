using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings" ,menuName = "Words")]
public class Words : ScriptableObject {

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
    [Header("Use common word")]
    private  bool useFewWord;
    [SerializeField]
    [Header("Use rare word")]
    private bool useLotWord;

    public List<string> words { get; set; }
    
    public void CreateList()
    {
        words = WordsFromText(text.text).Where(x => x.Length >= minLength).ToList();
        Game.MaxCountTrying = countTrying;
    }

    List<string> WordsFromText(string text)
    {
        List<string> words = new List<string>();

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

            if (word.Length != 0)
                words.Add(new string(word).ToUpper());
        }

        return words;
    }
}
