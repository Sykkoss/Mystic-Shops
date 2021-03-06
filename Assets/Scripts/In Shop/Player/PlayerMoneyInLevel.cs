﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyInLevel : MonoBehaviour
{
    public static PlayerMoneyInLevel Instance;

    public delegate void OnMoneyChangeDelegate(int newValue);
    public event OnMoneyChangeDelegate OnMoneyChange;

    public int CurrentMoney { get; private set; } = 0;


    private void Awake()
    {
        CurrentMoney = 0;
        Instance = this;
    }

    public void EarnMoney(int amount)
    {
        CurrentMoney += amount;
        TriggerMoneyChangeEvent();
    }

    public void LooseMoney(int amount)
    {
        CurrentMoney -= amount;
        TriggerMoneyChangeEvent();
    }

    private void TriggerMoneyChangeEvent()
    {
        OnMoneyChange(CurrentMoney);
    }
}
