using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rothnag {

public class EnsureTreeNeedsIsNotStopped : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Restarting TreeNeedsEventManager");
        TreeNeedsEventManager.instance.Restart();
    }
}

}
