using System.Collections;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using Unity.VisualScripting;
using UnityEngine;

public class D02 : InventoryItem
{
    public delegate void onD02Acquired();
    public static event onD02Acquired OnD02Acquired;

    public override bool Pick(string playerID)
    {
        OnD02Acquired?.Invoke();
        return true;
    }
}
