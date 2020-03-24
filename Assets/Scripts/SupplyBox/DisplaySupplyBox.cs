using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySupplyBox : MonoBehaviour
{
    public GameObject _emptyIndicator;
    public GameObject[] _items;


    public void DisplayEmptySlot(bool shouldDisplay)
    {
        _emptyIndicator.SetActive(shouldDisplay);
        foreach (GameObject item in _items)
            item.SetActive(!shouldDisplay);
    }
}
