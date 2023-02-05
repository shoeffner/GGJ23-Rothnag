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
        indicator.rectTransform.sizeDelta = new Vector2(indicator.rectTransform.sizeDelta.x, percentage * 100);
        indicator.rectTransform.anchoredPosition =
                new Vector2(indicator.rectTransform.anchoredPosition.x, percentage * 90 + -200);
    }
}