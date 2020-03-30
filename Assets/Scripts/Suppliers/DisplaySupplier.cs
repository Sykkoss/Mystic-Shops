using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySupplier : MonoBehaviour
{
    public GameObject _emptyIndicator;
    public Text _numberLeftIndicator;
    public GameObject[] _items;

    private ASupplier _supplier;


    private void Awake()
    {
        _supplier = GetComponent<ASupplier>();
        if (_supplier == null)
            Debug.LogError("Error: No 'Supplier' attached to the gameObject '" + gameObject.name + "'.");
    }

    private void OnGUI()
    {
        if (_supplier.CurrentCapacity <= 0)
            DisplayEmptySlot(true);
        else
            DisplayEmptySlot(false);
    }

    public void DisplayEmptySlot(bool shouldDisplay)
    {
        _emptyIndicator.SetActive(shouldDisplay);
        _numberLeftIndicator.text = _supplier.CurrentCapacity.ToString();
        foreach (GameObject item in _items)
            item.SetActive(!shouldDisplay);
    }
}
