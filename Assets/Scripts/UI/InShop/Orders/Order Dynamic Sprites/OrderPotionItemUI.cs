using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPotionItemUI : MonoBehaviour
{
    [System.Serializable]
    public struct PotionAssociation
    {
        public PotionColor color;
        public Sprite sprite;
    }

    public static OrderPotionItemUI Instance;
    public List<PotionAssociation> _potionsAssociations;


    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetPotionSprite(OrderItems.Potion potionItem)
    {
        foreach (PotionAssociation currentAssociation in _potionsAssociations)
        {
            if (currentAssociation.color == potionItem.Color)
                return currentAssociation.sprite;
        }
        return null;
    }
}
