using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Rothnag {

public class GameController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadScene("Home"));
    }

    IEnumerator LoadScene(string scene) {
        SceneManager.LoadSceneAsync(scene);
        yield return null;
    }
}

}
