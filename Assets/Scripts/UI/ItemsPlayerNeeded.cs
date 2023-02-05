using System;
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

    private void OnEnable()
    {
        foreach (TreeNeedsEvent treeNeedsEvent in _treeNeedsEventManager.treeNeedsEventsInUse)
            RegisterResetIndicator(treeNeedsEvent);
        _treeNeedsEventManager.OnEventStarted += RegisterResetIndicator;
    }

    private void OnDisable()
    {
        foreach (TreeNeedsEvent treeNeedsEvent in _treeNeedsEventManager.treeNeedsEventsInUse)
            UnregisterResetIndicator(treeNeedsEvent);
        _treeNeedsEventManager.OnEventStarted -= RegisterResetIndicator;
    }

    private void RegisterResetIndicator(TreeNeedsEvent treeNeedsEvent)
    {
        treeNeedsEvent.OnFailed += ResetIndicator;
        treeNeedsEvent.OnCompleted += ResetIndicator;
    }

    private void UnregisterResetIndicator(TreeNeedsEvent treeNeedsEvent)
    {
        treeNeedsEvent.OnFailed -= ResetIndicator;
        treeNeedsEvent.OnCompleted -= ResetIndicator;
    }

    private void ResetIndicator(TreeNeedsEvent treeNeedsEvent)
    {
        switch(treeNeedsEvent)
        {
            case TreeNeedsBirdhouseEvent:
                updateIndicator(birdhouseIndicator, 1f);
                break;
            case TreeNeedsSacrificeEvent:
                updateIndicator(animalIndicator, 1f);
                break;
            case TreeNeedsWaterEvent:
                updateIndicator(bucketIndicator, 1f);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(treeNeedsEvent));
            
        }

        UnregisterResetIndicator(treeNeedsEvent);
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
        if (indicator == null)
        {
            throw new Exception("indicator null");
        }
        if (percentage > 0.666)
        {
            indicator.color = red;
        } else if (percentage > 0.333) {
            indicator.color = yellow;
        } else {
            indicator.color = green;
        }
        indicator.rectTransform.sizeDelta = new Vector2(indicator.rectTransform.sizeDelta.x, percentage * 100);
        indicator.rectTransform.anchoredPosition =
                new Vector2(indicator.rectTransform.anchoredPosition.x, percentage * 90 + -200);
    }
}