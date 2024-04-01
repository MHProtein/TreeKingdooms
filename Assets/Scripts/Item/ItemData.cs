using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string item_name;

    [TextArea]
    public string description;
}
