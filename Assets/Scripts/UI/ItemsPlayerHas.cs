using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rothnag;

public class ItemsPlayerHas : MonoBehaviour
{
    public Image birdhouse;
    public Image animal;
    public Image bucket;

    void Update()
    {
        // listen to update events would be nicer but this is still a Game Jam
        birdhouse.enabled = PlayerInventory.instance.birdHouse > 0;
        animal.enabled = PlayerInventory.instance.animal > 0;
        bucket.enabled = PlayerInventory.instance.bucket > 0;
    }
}
