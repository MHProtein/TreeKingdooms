using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class N02 : MonoBehaviour
{
    [SerializeField] private DialogueSystemTrigger trigger1;
    [SerializeField] private DialogueSystemTrigger trigger2;

    private bool isTrigger1ed = false;
    private bool isTrigger2ed = false;

    private bool Talkable;

    public bool isD09Acquired;
    public bool isD09Used;

    void OnEnable()
    {
        D09.OnD09Acquired += OnD09Acquired;
    }

    void OnDisable()
    {
        D09.OnD09Acquired -= OnD09Acquired;
    }

    private void OnD09Acquired()
    {
        this.isD09Acquired = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        this.Talkable = true;
    }

    void OnTriggerExit2D()
    {
        this.Talkable = false;
    }

    public void SetD09Used()
    {
        this.isD09Used = true;
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
            else if (this.isTrigger1ed && !this.isTrigger2ed && this.isD09Acquired && this.isD09Used)
            {
                this.trigger2.OnUse();
                this.isTrigger2ed = true;
                Scene1Manager.conversedWithSoilder = true;
                this.trigger1.trigger = DialogueSystemTriggerEvent.None;
                this.trigger2.trigger = DialogueSystemTriggerEvent.None;
            }
        }
    }
}
