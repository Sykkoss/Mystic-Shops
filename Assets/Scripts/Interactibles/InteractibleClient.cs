using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleClient : MonoBehaviour, IInteractible
{
    public GameObject _potionPrefab;

    public void Interact(ICustomItem item)
    {
        CustomPotion potion = (CustomPotion)item;

        print("Interacted");
        Destroy(potion.gameObject);
        Instantiate(_potionPrefab);
    }
}
