using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Tymski;

namespace Rothnag {

public class SwitchToScene : MonoBehaviour
{
    public SceneReference scene;
    public Collider2D triggerer;

    void OnEnable() {
        if (triggerer == null) {
            triggerer = GameObject.Find("CharacterPrefab").GetComponent<Collider2D>();
        }
    }

    void Awake() {
        if (scene == null) {
            Debug.LogError("No SceneReference provided");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other != triggerer) {
            return;
        }
        SceneManager.LoadSceneAsync(scene);
        return;
    }
}

}
