using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISupplier
{
    void SetMaxCapacity(int maxCapacity);

    bool DecrementCurrentCapacity();
}
