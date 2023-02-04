using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tymski;


namespace Rothnag
{

public class ScoreKeeper : MonoBehaviour
{
    public int loseOnFailure = 3;
    public SceneReference sceneOnFailure;

    public int failures = 0;
    private bool failed = false;

    void Update() {
        if (!failed && failures >= loseOnFailure) {
            failed = true;
            SceneManager.LoadSceneAsync(sceneOnFailure);
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        TreeNeedsEventManager.instance.OnEventStarted += NewTreeNeed;
    }

    void OnDisable()
    {
        TreeNeedsEventManager.instance.OnEventStarted -= NewTreeNeed;
    }

    void NewTreeNeed(TreeNeedsEvent treeNeedsEvent) {
        treeNeedsEvent.OnFailed += FailedNeedsEvent;
    }

    void FailedNeedsEvent(TreeNeedsEvent treeNeedsEvent) {
        treeNeedsEvent.OnFailed -= FailedNeedsEvent;
        failures = Mathf.Clamp(failures + 1, 0, loseOnFailure);
    }
}

}
