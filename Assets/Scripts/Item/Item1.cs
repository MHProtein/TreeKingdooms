using System.Collections;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using UnityEngine;

public class Item1 : InventoryItem
{
    public delegate void onPickedUp();

    public static event onPickedUp OnPickedUp;
    public override bool Pick(string playerID)
    {
        OnPickedUp?.Invoke();

        return true;
    }
}
