using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using MoreMountains.InventoryEngine;

public enum ItemType
{
    INTERACTABLE,
    PICKABLE,
    ALTERABLE
}

public class Item : MonoBehaviour, IInteractable
{
    public ItemType type;
    public string id;
    [TextArea] public string[] subtitles;
    public Sprite newSprite;
    public bool allowMultipleInteractions = false;
    public bool forcedInteraction = false;
    [HideInInspector] public bool interactable = false;
    [HideInInspector]public bool interected = false;
    public bool entered = false;

    private bool exited = false;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject subtitleManager;

    public delegate void onItemInteracted(string id);

    public static event onItemInteracted OnItemInteracted;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("entered " + this.name);
            this.entered = true;
            this.exited = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("exited " + this.name);
            this.exited = true;
            this.entered = false;
        }
    }

    public void Interact()
    {
        switch (type)
        {
            case ItemType.INTERACTABLE:
            {
                if(!this.allowMultipleInteractions)
                    if(this.interected)
                        return;
                Debug.Log("Interacting with INTERACTABLE " + this.name);
                this.subtitleManager.GetComponent<SubtitleManager>().StartDisplaying(subtitles, id);
                OnItemInteracted?.Invoke(id);
                break;
            }
            case ItemType.PICKABLE:
            {
                this.subtitleManager.GetComponent<SubtitleManager>().StartDisplaying(subtitles, id);
                var picker = GetComponent<ItemPicker>();
                picker.Pick();
                this.parent.SetActive(false);
                OnItemInteracted?.Invoke(id);
                Debug.Log("Interacting with PICKABLE " + this.name);
                break;
            }
            case ItemType.ALTERABLE:
            {
                if (this.interected)
                    return;
                this.subtitleManager.GetComponent<SubtitleManager>().StartDisplaying(subtitles, id);
                Debug.Log("Interacting with ALTERABLE " + this.name);
                this.renderer.sprite = this.newSprite;
                OnItemInteracted?.Invoke(id);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
        this.interected = true;
    }
}
