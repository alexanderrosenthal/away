using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Sprite sailUpSprite;
    [SerializeField] private Sprite sailDownSprite;
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
        // TODO SOUND
    }
    
    public void LowerSail()
    {
       spriteRenderer.sprite = sailDownSprite;
    }

    public void RaiseSail()
    {
        spriteRenderer.sprite = sailUpSprite;
    }
}