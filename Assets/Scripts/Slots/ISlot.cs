using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASlot : MonoBehaviour
{
    public bool IsOccupied { get; set; }


    public void FreeSlot()
    {
        IsOccupied = false;
    }

    public abstract void AssignSlot(ACustomItem item);
}
