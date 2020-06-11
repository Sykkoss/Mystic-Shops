using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPainterSupplier : ASupplier
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

    public void PaintMask(CustomYokaiMask yokaiMask, YokaiMaskPaint newPaint)
    {
        if (!_isOccupied)
            StartCoroutine(Filling(yokaiMask, newPaint));
        else
            Debug.Log("Info: 'MaskPainterSupplier.PaintMask(...)' was called but valve was occupied.");
    }

    private IEnumerator Filling(CustomYokaiMask yokaiMask, YokaiMaskPaint newPaint)
    {
        _isOccupied = true;

        //TODO: Start animation
        yield return new WaitForSeconds(_fillingTime);

        yokaiMask._potionUpdateSprite.ChangeSpriteColor((PotionColor)yokaiMask.PaintType, (PotionColor)newPaint);
        yokaiMask.PaintType = newPaint;
        yokaiMask.ResetPositionToSlot();
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
