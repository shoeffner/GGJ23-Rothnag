using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Rothnag {

public sealed class SceneTracker
{
    public string lastScene;

    private SceneTracker() {}
    private static SceneTracker instance = null;
    public static SceneTracker Instance {
        get {
            if (instance == null) {
                instance = new SceneTracker();
            }
            return instance;
        }
    }
}

public class TrackScene : MonoBehaviour {
    void Awake() {
        SceneTracker.Instance.lastScene = SceneManager.GetActiveScene().name;
    }
}

}
