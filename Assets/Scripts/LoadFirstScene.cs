using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tymski;

namespace Rothnag {

public class LoadFirstScene : MonoBehaviour
{
    public SceneReference scene;

    void Awake()
    {
        TreeNeedsEventManager.instance.Restart();
        SceneManager.LoadSceneAsync(scene);
    }
}

}
