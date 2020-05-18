using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveSupplier : ASupplier
{
    public float _fillingTime;
    public int _maxCapacity;

    [HideInInspector]
    public bool _isOccupied;


    private void Start()
    {
        _isOccupied = false;
        SetMaxCapacity(_maxCapacity);
    }

    public override void Tapped(Vector3 positionTapped)
    {
        if (CurrentCapacity <= 0)
            StartCoroutine(Refilling());
    }

    public void FillPotion(CustomPotion potion, PotionColor newColor)
    {
        if (!_isOccupied)
            StartCoroutine(Filling(potion, newColor));
        else
            Debug.LogError("Error: 'ValveSupplier.FillPotion(...)' was called but valve was occupied.");
    }

    private IEnumerator Filling(CustomPotion potion, PotionColor newColor)
    {
        _isOccupied = true;

        //TODO: Start animation
        yield return new WaitForSeconds(_fillingTime);

        potion._potionUpdateSprite.ChangeSpriteColor(potion.Color, newColor);
        potion.Color = newColor;
        potion.ResetPositionToSlot();
        DecrementCurrentCapacity();

        _isOccupied = false;
    }

    private IEnumerator Refilling()
    {
        SetIsRefilling(true);

        //TODO: Start animation
        yield return new WaitForSeconds(_refillTime);

        ResetCurrentCapacity();

        SetIsRefilling(false);
    }

}
