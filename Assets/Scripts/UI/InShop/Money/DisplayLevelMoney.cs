using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLevelMoney : MonoBehaviour
{
    public Text _moneyValueText;

    private PlayerMoneyInLevel _playerMoneyInLevel;
    private Coroutine _updateMoneyCoroutine;


    private void Start()
    {
        _playerMoneyInLevel = PlayerMoneyInLevel.Instance;
        if (_playerMoneyInLevel == null)
            Debug.LogError("Error: Instance of 'PlayerMoneyInLevel' not set.");
        else
            _playerMoneyInLevel.OnMoneyChange += DisplayNewMoneyValue;
    }

    private void DisplayNewMoneyValue(int newValue)
    {
        if (_updateMoneyCoroutine != null)
            StopCoroutine(_updateMoneyCoroutine);
        _updateMoneyCoroutine = StartCoroutine(UpdateMoneyCounter(newValue));
    }

    private IEnumerator UpdateMoneyCounter(int finalValue)
    {
        int currentMoney = int.Parse(_moneyValueText.text);
        float waitTime = 0.2f / (Mathf.Abs(finalValue - currentMoney) / 3);

        while (currentMoney != finalValue)
        {
            currentMoney += (finalValue > currentMoney) ? (1) : (-1);
            _moneyValueText.text = currentMoney.ToString();
            yield return new WaitForSeconds(waitTime);
        }
    }
}
