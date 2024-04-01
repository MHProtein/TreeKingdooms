using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class N06 : MonoBehaviour
{
    [SerializeField] private DialogueSystemTrigger trigger;

    private bool Talkable = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.Talkable)
            return;
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (Scene3Manager.D16Interected && Scene3Manager.D18Interected && 
                Scene3Manager.D19Interected && Scene3Manager.talked)
            {
                Debug.Log("1");
                this.trigger.trigger = DialogueSystemTriggerEvent.OnUse;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.Talkable = true;
    }

    void OnTriggerExit2D()
    {
        this.Talkable = false;
    }

}
