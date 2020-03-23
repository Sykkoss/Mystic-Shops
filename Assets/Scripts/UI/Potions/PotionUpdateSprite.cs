using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionUpdateSprite : MonoBehaviour
{
    public Sprite[] _potionSprites;

    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSpriteColor(PotionColor oldColor, PotionColor actualColor)
    {
        //TODO: Check old color and new, then animate depending on from what to what color it is
        // When animations will have to be done, maybe only send a trigger to the animator that will follow a natural path with animations links ?

        _spriteRenderer.sprite = _potionSprites[(int)actualColor];
    }
}
