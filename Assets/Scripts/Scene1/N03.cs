using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class N03 : MonoBehaviour
{
    [SerializeField] private DialogueSystemTrigger trigger1;
    [SerializeField] private DialogueSystemTrigger trigger2;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject radio;
    [SerializeField] private SubtitleManager subtitleManager;

    private bool isTrigger1ed = false;
    private bool isTrigger2ed = false;

    private bool Talkable;

    public bool isD09Acquired;

    void OnEnable()
    {
        if (Scene1Manager.conversedWithSoilder)
        {
            Debug.Log("trigger1");
            this.trigger1.OnUse();
        }
        else
        {
            Debug.Log("trigger2");
            this.trigger2.OnUse();
        }
    }

    public void OnConversationEnd(Transform actor)
    {
        List<string> str = new List<string>();
        str.Add("获得物品*广播喇叭*");
        this.parent.SetActive(false);
        this.subtitleManager.StartDisplaying(str.ToArray(), "Guan");
        Scene1Manager.RadioGained = true;
        this.radio.SetActive(false);
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
        
    }
}
