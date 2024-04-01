using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleTrigger : MonoBehaviour
{

    [TextArea] public string[] texts;

    [SerializeField] private GameObject subtitleManager;

    private bool Displayed = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.Displayed)
            return;
        if (!other.CompareTag("Player"))
            return;
        this.subtitleManager.GetComponent<SubtitleManager>().StartDisplaying(this.texts, "id");
        this.Displayed = true;
    }

}
