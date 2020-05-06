using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySupplier : MonoBehaviour
{
    public Image _emptyIndicator;
    public Text _numberLeftIndicator;
    public GameObject[] _items;

    private ASupplier _supplier;
    private bool _isShowingRefillAnim;


    private void Awake()
    {
        _isShowingRefillAnim = false;
        _supplier = GetComponent<ASupplier>();
        if (_supplier == null)
            Debug.LogError("Error: No 'Supplier' attached to the gameObject '" + gameObject.name + "'.");
    }

    private void OnGUI()
    {
        if (_supplier.CurrentCapacity <= 0)
        {
            DisplayEmptySlot(true);
            if (!_isShowingRefillAnim && _supplier.IsRefilling)
            {
                _isShowingRefillAnim = true;
                StartCoroutine(DisplayRefillGauge.DecreaseRefillGaugeFor(_emptyIndicator, _supplier._refillTime));
            }
        }
        else
        {
            _isShowingRefillAnim = false;
            DisplayEmptySlot(false);
        }
    }

    public void DisplayEmptySlot(bool shouldDisplay)
    {
        _emptyIndicator.gameObject.SetActive(shouldDisplay);
        _numberLeftIndicator.text = _supplier.CurrentCapacity.ToString();
        foreach (GameObject item in _items)
            item.SetActive(!shouldDisplay);
    }
}
