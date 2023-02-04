using System;
using Rothnag.TreeNeedsEvents;

namespace Rothnag
{
    public sealed class InteractWithTree : Interactable
    {
        protected override void InteractionBehaviour()
        {
            TreeNeedsEventManager treeNeedsEventManager = TreeNeedsEventManager.instance;
            PlayerInventory playerInventory = PlayerInventory.instance;
            TreeNeedsEvent[] treeNeedsEvents = treeNeedsEventManager.treeNeedsEventsInUse.ToArray();
            foreach (TreeNeedsEvent treeNeedsEvent in treeNeedsEvents)
            {
                switch (treeNeedsEvent)
                {
                    case TreeNeedsSacrificeEvent:
                        if (playerInventory.animal > 0)
                        {
                            playerInventory.animal = 0;
                            treeNeedsEvent.progress++;
                        }

                        break;
                    case TreeNeedsWaterEvent:
                        if (playerInventory.bucket > 0)
                        {
                            playerInventory.bucket = 0;
                            treeNeedsEvent.progress++;
                        }

                        break;
                    case TreeNeedsBirdhouseEvent:
                        if (playerInventory.birdHouse > 0)
                        {
                            playerInventory.birdHouse = 0;
                            treeNeedsEvent.progress++;
                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(treeNeedsEvent));
                }
            }
        }
    }
}