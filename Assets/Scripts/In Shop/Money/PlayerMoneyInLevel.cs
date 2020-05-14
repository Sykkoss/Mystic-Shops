using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyInLevel : MonoBehaviour
{
    public static PlayerMoneyInLevel Instance;

    public delegate void OnMoneyChangeDelegate(int newValue);
    public event OnMoneyChangeDelegate OnMoneyChange;

    public int CurrentMoney { get; private set; }


    private void Awake()
    {
        CurrentMoney = 0;
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            EarnMoney(50);
        else if (Input.GetKeyDown(KeyCode.L))
            LooseMoney(50);
    }

    public void EarnMoney(int amount)
    {
        CurrentMoney += amount;
        TriggerMoneyChangeEvent();
    }

    public void LooseMoney(int amount)
    {
        CurrentMoney -= amount;
        if (CurrentMoney < 0)
            CurrentMoney = 0;
        TriggerMoneyChangeEvent();
    }

    private void TriggerMoneyChangeEvent()
    {
        OnMoneyChange(CurrentMoney);
    }
}
