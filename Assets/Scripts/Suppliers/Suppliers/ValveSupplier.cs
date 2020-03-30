using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveSupplier : ASupplier
{
    private void Start()
    {
        SetMaxCapacity(2);
    }

    public void FillPotion()
    {
        DecrementCurrentCapacity();
    }

    public override void Tapped(Vector3 positionTapped)
    {
        if (CurrentCapacity <= 0)
            ResetCurrentCapacity();
    }
}
