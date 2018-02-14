using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButton : MonoBehaviour
{

    private static int count = 0;

    private char letter;

    void Start()
    {
        Game.onEnable += onEnable;
        GetComponent<Button>().onClick.AddListener(OnClick);
        letter = (char)('A' + count);
        GetComponentInChildren<Text>().text = letter.ToString();
        count++;
    }

    public void onEnable()
    {
        if (gameObject.activeSelf == false)
            transform.gameObject.SetActive(true);
    }

    void OnClick()
    {
        gameObject.SetActive(false);
        WordInGame.Instance.GetLetter(letter);
    }
}
