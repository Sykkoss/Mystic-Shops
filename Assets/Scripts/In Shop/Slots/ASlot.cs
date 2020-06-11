using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASlot : MonoBehaviour
{
    public bool IsOccupied { get; set; }
    public Vector2 SlotPosition { get; private set; }


    private void Awake()
    {
        SlotPosition = transform.position;
    }

    public void FreeSlot()
    {
        IsOccupied = false;
    }
}
