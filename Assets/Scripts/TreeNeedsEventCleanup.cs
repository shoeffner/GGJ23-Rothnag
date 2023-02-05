using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using Tymski;
using Input;

namespace Rothnag {

public class TreeNeedsEventCleanup : MonoBehaviour
{
    public SceneReference restartScene;
    private RothnagInputActionAsset.ScoreScreenActions inputs;

    void Awake()
    {
        inputs = InputProvider.instance.ScoreScreen;
    }

    void OnEnable()
    {
        inputs.Restart.performed += Restart;
    }

    void OnDisable()
    {
        inputs.Restart.performed -= Restart;
    }

    void Start()
    {
        TreeNeedsEventManager.instance.Stop();

        PlayerInventory playerInventory = FindAnyObjectByType<PlayerInventory>();
        if (playerInventory != null) {
            playerInventory.bucket = 0;
            playerInventory.animal = 0;
            playerInventory.birdHouse = 0;
        }

        TreeNeedsEvent[] treeNeedsEvents = FindObjectsByType<TreeNeedsEvent>(FindObjectsSortMode.None);
        foreach (TreeNeedsEvent evt in treeNeedsEvents)
        {
            Destroy(evt.gameObject);
        }
    }

    public void Restart(InputAction.CallbackContext cb)
    {
        Destroy(this);
    }

    void OnDestroy()
    {
        DontDestroyOnLoad[] ddols = FindObjectsByType<DontDestroyOnLoad>(FindObjectsSortMode.None);
        foreach (DontDestroyOnLoad ddol in ddols) {
            Destroy(ddol.gameObject);
        }
        SceneManager.LoadSceneAsync(restartScene);
    }
}

}
