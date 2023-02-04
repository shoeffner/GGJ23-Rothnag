using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rothnag {

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

}
