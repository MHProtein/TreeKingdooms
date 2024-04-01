using System.Collections;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using Unity.VisualScripting;
using UnityEngine;

public class D09 : InventoryItem
{
    public delegate void onD09Acquired();
    public static event onD09Acquired OnD09Acquired;

    public override bool Pick(string playerID)
    {
        OnD09Acquired?.Invoke();
        Scene1Manager.WalletGained = true;
        return true;
    }
}