using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites;
    private int numOfSprites;
    private void Start()
    {
        numOfSprites = sprites.Count;
    }

    public void SetSprite(float percentage)
    {
        int index = Mathf.FloorToInt(percentage * numOfSprites);
        if (index == numOfSprites)
        {
            index--;
        }
        spriteRenderer.sprite = sprites[index];
        print(index);
        print((int)index);
        // TODO SOUND
    }
}
