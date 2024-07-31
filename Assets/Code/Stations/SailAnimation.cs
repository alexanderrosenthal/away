using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Sprite sailUpSprite;
    private int numOfSprites;
    // private bool sailSet;
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
        // TODO sail up sprite
    }

    /*
    public void LowerSail()
    {
        // sailSet = true;
    }
    */

    public void RaiseSail()
    {
        // sailSet = false;
        spriteRenderer.sprite = sailUpSprite;
    }
}
