using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

    public Sequence[] seq;


    void Start() {
        if (seq.Length < 1) {
            Debug.LogError("No sequences to play");
        }

        StartCoroutine("PlaySequences");
    }

    IEnumerator PlaySequences() {
        Scene curScene = new Scene();
        for (int i = 0; i < seq.Length; i++) {
            AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(seq[i].name, LoadSceneMode.Additive);

            // wait until asynchronus scene fully loads
            while (!asyncLoad.isDone) {
                yield return null;
            }

            // unload previous scene if there is one
            if (i != 0) {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(curScene);
            }

            curScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(seq[i].name);
            yield return new WaitForSecondsRealtime(seq[i].length);

        }

       Debug.Log("Movie Completed");
    }
}


[System.Serializable]
public struct Sequence {
    public string name;

    // seconds
    [Tooltip("seconds")]
    public float length;
}
