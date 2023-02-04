using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rothnag {

public class TreeAnger : MonoBehaviour
{
    private ScoreKeeper scoreKeeper = null;
    public SpriteRenderer spriteRenderer = null;
    public List<Sprite> sprites = new List<Sprite>();
    private int currentSprite = 0;

    void Awake()
    {
        scoreKeeper = GameObject.FindWithTag("ScoreKeeper")?.GetComponent<ScoreKeeper>();
        spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        SetTreeSprite();
    }

    void Update()
    {
        SetTreeSprite();
    }

    void SetTreeSprite() {
        if (scoreKeeper != null && scoreKeeper.failures != currentSprite) {
            currentSprite = Mathf.Clamp(scoreKeeper.failures, 0, sprites.Count);
            spriteRenderer.sprite = sprites[currentSprite];
        }
    }
}

}
