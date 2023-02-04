using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StartBucketMiniGame : MonoBehaviour
{
    public Collider2D triggerer;
    public BucketMiniGame miniGame;

    public void OnEnable()
    {
        if (triggerer == null) {
            triggerer = GameObject.Find("CharacterPrefab").GetComponent<Collider2D>();
        }
        if (miniGame == null) {
            miniGame = GameObject.Find("bucket").GetComponent<BucketMiniGame>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider == triggerer) {
            miniGame.PreStartGame();
        }
    }
}
