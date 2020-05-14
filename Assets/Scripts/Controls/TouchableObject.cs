using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchableObject : MonoBehaviour
{

    public void Touching()
    {
        ATouchItem touchItem;

        if (TryGetComponent<ATouchItem>(out touchItem))
            touchItem.Touch();
    }
}
