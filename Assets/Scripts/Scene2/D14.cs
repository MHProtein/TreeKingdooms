using System.Collections;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using UnityEngine;

public class D14 : InventoryItem
{
    public delegate void onHammerGained();
    public static event onHammerGained OnHammerGained;

    public override bool Pick(string playerID)
    {
        OnHammerGained?.Invoke();
        return true;
    }
}
