using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPileOfCoins : ATouchItem
{
    public int CoinsValue { get; set; }
    public ClientSlot ClientSlot { get; set; }

    public override void Touch()
    {
        PlayerMoneyInLevel.Instance.EarnMoney(CoinsValue);
        ClientSlot.FreeSlot();
        Destroy(this.gameObject);
    }
}
