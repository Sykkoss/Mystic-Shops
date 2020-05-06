using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSlot : ASlot
{
    public void AssignClientSlot(Client client)
    {
        client.SetSlot(this);
        IsOccupied = true;
    }
}
