using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData itemData;
    private MoreMountains.InventoryEngine.Inventory inventory;
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    public void InitializeItem(ItemData item_data)
    {
        this.itemData = item_data;
    }

    void Start()
    {
        InitializeItem(this.itemData);
    }

    void Update()
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag"); 
        this.image.raycastTarget = false;
        this.parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
     
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        this.image.raycastTarget = true;
        transform.SetParent(this.parentAfterDrag);
    }
}
