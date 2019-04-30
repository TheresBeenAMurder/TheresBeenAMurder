using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneLoader : MonoBehaviour
{

    public AutoConversation chiefConvo;

    AsyncOperation async;
    public bool nextSceneLoaded = false;
    public bool moveOn = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNext());
        StartCoroutine(chiefConvo.PlayDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if(!nextSceneLoaded)
        {
            if(async.isDone)
            {
                nextSceneLoaded = true;
            }
        }

        if(!moveOn)
        {
            if(chiefConvo.IsFinished())
            {
                moveOn = true;
            }
        }

        if(nextSceneLoaded && moveOn)
        {
            async.allowSceneActivation = true;
        }
    }

    IEnumerator LoadNext()
    {

        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        async.allowSceneActivation = false;
        yield return async;
    }
}
