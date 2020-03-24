using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSupplyBox : ASupplyBox
{
    private void Start()
    {
        SetMaxCapacity(5);
        _displaySupplyBox = GetComponent<DisplaySupplyBox>();
    }

    public override void DecrementCurrentCapacity()
    {
        CurrentCapacity -= 1;
        if (CurrentCapacity <= 0)
            _displaySupplyBox.DisplayEmptySlot(true);
    }
}
