using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSlot : ASlot
{
    [HideInInspector]
    public Vector2 _pileOfCoinPosition;


    private void Awake()
    {
        _pileOfCoinPosition = transform.GetChild(1).transform.position;
        if (_pileOfCoinPosition == null)
            Debug.LogError("Error: No pile of coin child slot for gameObject '" + gameObject.name + "'.");
    }

    public void AssignClientSlot(Client client)
    {
        client.SetSlot(this);
        IsOccupied = true;
    }
}
