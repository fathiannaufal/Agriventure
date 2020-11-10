using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenegement : MonoSingleton<SceneMenegement>
{
    public Animator transition;
    public float transitionTime = 1f;

    public void crossFade(string sceneIndex, bool bol)
    {
        if(bol)
            SaveGame.instance.saveLastPlace(sceneIndex);
        StartCoroutine(loadScene(sceneIndex));
        Debug.Log("crossfade");
    }

    IEnumerator loadScene(string sceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
