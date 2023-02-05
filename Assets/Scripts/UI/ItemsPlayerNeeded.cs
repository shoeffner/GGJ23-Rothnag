using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rothnag;

public class ItemsPlayerNeeded : MonoBehaviour
{
    public Image birdhouseIndicator;
    public Image animalIndicator;
    public Image bucketIndicator;

    private float testPercentage = 1.0f;

    void Update()
    {
        // listen to update events would be nicer but this is still a Game Jam
        testPercentage -= 0.01f;
        if (testPercentage <= 0) {
            testPercentage = 1f;
        }
        updateIndicator(bucketIndicator, testPercentage);
        updateIndicator(animalIndicator, testPercentage);
        updateIndicator(birdhouseIndicator, testPercentage);

    }

    void updateIndicator(Image indicator, float percentage) {
        indicator.rectTransform.sizeDelta = new Vector2(indicator.rectTransform.sizeDelta.x, percentage * 100);
        indicator.rectTransform.anchoredPosition = new Vector2(indicator.rectTransform.anchoredPosition.x, percentage * 90 + -200);

    }
}
