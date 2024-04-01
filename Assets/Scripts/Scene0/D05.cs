using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D05 : MonoBehaviour
{
    public delegate void onD05Interacted();
    public static event onD05Interacted OnD05Interacted;

    [SerializeField] private Item item;
    [SerializeField] private N01 n01;
    private bool told;

    void Update()
    {
        if (this.told)
            return;
        if (this.item.interected)
        {
            OnD05Interacted?.Invoke();
        }
    }

}
