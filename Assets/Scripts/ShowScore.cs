using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rothnag {

public class ShowScore : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Awake()
    {
        scoreText.text = scoreText.text.Replace("{SCORE}", Time.realtimeSinceStartup.ToString("0"));
    }
}

}
