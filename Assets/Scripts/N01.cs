using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static D02;

public class N01 : MonoBehaviour
{
    [SerializeField] private DialogueSystemTrigger trigger1;
    [SerializeField] private DialogueSystemTrigger trigger2;
    [SerializeField] private DialogueSystemTrigger trigger3;
    [SerializeField] private SpriteRenderer N01Renderer;
    [SerializeField] private SpriteRenderer N05Renderer;
    [SerializeField] private MoreMountains.InventoryEngine.Inventory inventory;

    private bool isTrigger1ed = false;
    private bool isTrigger2ed = false;
    private bool isTrigger3ed = false;

    private bool Talkable;

    public bool isD02Used = false;
    public bool isD05Interacted = false;
    public Sprite N01_1;
    public Sprite N05_1;

    public delegate void onAccessoryRetrived();
    public static event onAccessoryRetrived OnAccessoryRetrived;


    public void D02Used()
    {
        this.isD02Used = true;
    }

    void OnEnable()
    {
        Item.OnItemInteracted += OnItemInteracted;
    }

    void OnDisable()
    {
        Item.OnItemInteracted -= OnItemInteracted;
    }

    private void OnItemInteracted(string id)
    {
        if (id == "D05")
            this.isD05Interacted = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.Talkable = true;
    }

    void OnTriggerExit2D()
    {
        this.Talkable = false;
    }

    void Start()
    {
    }

    void Update()
    {
        if (!Scene0Manager.Talkable)
            return;
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (!this.isTrigger1ed)
            {
                this.trigger1.OnUse();
                this.isTrigger1ed = true;
            }
            else if (this.isTrigger1ed && !this.isTrigger2ed && this.isD02Used)
            {
                this.trigger2.OnUse();
                N01Renderer.sprite = this.N01_1;
                this.isTrigger2ed = true;
                this.inventory.RemoveItemByID("D02", 1);
                OnAccessoryRetrived?.Invoke();
                this.trigger1.trigger = DialogueSystemTriggerEvent.None;
            }
            else if (this.isTrigger2ed && this.isD05Interacted)
            {
                this.trigger3.OnUse();
                this.N05Renderer.sprite = this.N05_1;
                Scene0Manager.Scene0Cleared = true;
                this.trigger1.trigger = DialogueSystemTriggerEvent.None;
                this.trigger2.trigger = DialogueSystemTriggerEvent.None;
            }
        }
    }
}
