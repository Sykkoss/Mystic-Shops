using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASupplier : MonoBehaviour, ISupplier
{
    public int MaxCapacity { get; private set; } = 0;
    public int CurrentCapacity { get; private set; } = 0;


    #region Capacity modifiers

    public void SetMaxCapacity(int maxCapacity)
    {
        MaxCapacity = maxCapacity;
        CurrentCapacity = MaxCapacity;
    }

    protected void ResetCurrentCapacity()
    {
        CurrentCapacity = MaxCapacity;
    }

    /// <summary>
    /// Returns true if supplier is empty. False otherwise
    /// </summary>
    public bool DecrementCurrentCapacity()
    {
        CurrentCapacity -= 1;
        if (CurrentCapacity <= 0)
            return true;
        return false;
    }

    #endregion Capacity modifiers

    public abstract void Tapped(Vector3 positionTapped);
}
