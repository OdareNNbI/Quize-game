using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    AsyncOperation async;

    IEnumerator Start()
    {
        KeyboardButton.count = 0;
        async = SceneManager.LoadSceneAsync("Main");
        async.allowSceneActivation = false;
        yield return async;
    }


    public void Load()
    {
        async.allowSceneActivation = true;
    }
}
