using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rothnag;
using Rothnag.TreeNeedsEvents;

public sealed class ItemsPlayerNeeded : MonoBehaviour
{
    public Image birdhouseIndicator;
    public Image animalIndicator;
    public Image bucketIndicator;

    private Color32 red = new Color32(0,255,0,100);
    private Color32 yellow = new Color32(255,255,0,100);
    private Color32 green = new Color32(255,0,0,100);

    private float testPercentage = 1.0f;
    private TreeNeedsEventManager _treeNeedsEventManager;

    private void Awake()
    {
        _treeNeedsEventManager = TreeNeedsEventManager.instance;
        // init indicators
        updateIndicator(birdhouseIndicator, 1f);
        updateIndicator(animalIndicator, 1f);
        updateIndicator(bucketIndicator, 1f);
    }

    void Update()
    {
        foreach (TreeNeedsEvent treeNeedsEvent in _treeNeedsEventManager.treeNeedsEventsInUse)
        {
            float timeLeftRatio = treeNeedsEvent.timeLeft / treeNeedsEvent.timeLimit;
            switch (treeNeedsEvent)
            {
                case TreeNeedsBirdhouseEvent:
                    updateIndicator(birdhouseIndicator, timeLeftRatio);
                    break;
                case TreeNeedsSacrificeEvent:
                    updateIndicator(animalIndicator, timeLeftRatio);
                    break;
                case TreeNeedsWaterEvent:
                    updateIndicator(bucketIndicator, timeLeftRatio);
                    break;
            }
        }
    }

    private void updateIndicator(Image indicator, float percentage)
    {
        if (percentage > 0.666) {
            indicator.GetComponent<Image>().color = red;
        } else if (percentage > 0.333) {
            indicator.GetComponent<Image>().color = yellow;
        } else {
            indicator.GetComponent<Image>().color = green;
        }
        indicator.rectTransform.sizeDelta = new Vector2(indicator.rectTransform.sizeDelta.x, percentage * 100);
        indicator.rectTransform.anchoredPosition =
                new Vector2(indicator.rectTransform.anchoredPosition.x, percentage * 90 + -200);
    }
}