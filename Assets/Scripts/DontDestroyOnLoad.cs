using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rothnag {

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

}
