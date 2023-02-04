using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rothnag {

public class BirdhouseCollectible : MonoBehaviour
{
    public Collider2D collector;

    void Awake()
    {
        if (PlayerInventory.instance.birdHouse > 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider != collector) {
            return;
        }
        PlayerInventory.instance.birdHouse = 1;
        Destroy(gameObject);
    }
}

}
