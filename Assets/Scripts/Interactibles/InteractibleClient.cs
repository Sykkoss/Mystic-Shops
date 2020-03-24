using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleClient : MonoBehaviour, IInteractible
{
    public GameObject _potionPrefab;

    public void Interact(ACustomItem item)
    {
        CustomPotion potion = (CustomPotion)item;

        Destroy(potion.gameObject);
    }
}
