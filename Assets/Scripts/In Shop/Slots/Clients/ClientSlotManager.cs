using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSlotManager : MonoBehaviour
{
    public static ClientSlotManager Instance;

    private List<ClientSlot> _slots;


    private void Awake()
    {
        Instance = this;
    }

    public void InitSlots()
    {
        ClientSlot currentSlot;

        _slots = new List<ClientSlot>();
        foreach (Transform child in transform)
        {
            currentSlot = child.GetComponent<ClientSlot>();
            currentSlot.InitSlot();
            if (currentSlot != null)
                _slots.Add(currentSlot);
        }
    }

    public bool AssignRandomSlotAvailable(Client client)
    {
        int randomSlotIndex;
        bool hasAssigned = false;
        List<ClientSlot> allSlots = new List<ClientSlot>();

        allSlots.AddRange(_slots);
        while (!hasAssigned && allSlots.Count > 0)
        {
            randomSlotIndex = Random.Range(0, allSlots.Count);
            if (!allSlots[randomSlotIndex].IsOccupied)
            {
                allSlots[randomSlotIndex].AssignClientSlot(client);
                hasAssigned = true;
                break;
            }
            else
                allSlots.RemoveAt(randomSlotIndex);
        }

        if (!hasAssigned)
        {
            client.DestroyClient();
            return false;
        }
        return true;
    }
}
