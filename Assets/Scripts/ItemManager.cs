using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public List<GameObject> items;

    [SerializeField] private GameObject player;
    [SerializeField] private TheSceneManager sceneManager;
    void Start()
    {
    }
  
    void Update()
    {
        if(TheSceneManager.talking)
            return;
        if (TheSceneManager.interactable == false)
            return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Item closestItem = GetClosestItem();
            
            if (closestItem != null)
            {
                closestItem.Interact();
            }
        }
    }

    private Item GetClosestItem()
    {
        Item closestItem = null;
        float closestDistance = float.MaxValue;

        foreach (var item in items)
        {
            var it = item.GetComponent<Item>();
            if (it.forcedInteraction && it.entered)
            {
                return it;
            }

            if (!it.allowMultipleInteractions && it.interected)
            {
                continue;
            }

            if (!it.entered)
            {
                continue;
            }

            float distanceToPlayer = Vector3.Distance(player.transform.position, item.transform.position);

            if (distanceToPlayer < closestDistance)
            {
                closestItem = it;
                closestDistance = distanceToPlayer;
            }
        }

        return closestItem;
    }

}
