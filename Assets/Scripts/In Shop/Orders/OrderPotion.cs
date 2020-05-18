using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderItems
{
    public interface OrderItem
    {
        System.Type Type { get; }
        int Complexity { get; set; }
        bool IsGiven { get; set; }
    }


    public class Potion : OrderItem
    {
        public System.Type Type { get { return typeof(CustomPotion); } }
        public int Complexity { get; set; }
        public bool IsGiven { get; set; }
        public PotionColor Color { get; set; }
    }
}
