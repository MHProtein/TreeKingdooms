using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static D14;

public class D11 : MonoBehaviour
{
    public List<Sprite> sprites;

    private List<string> subtitles = new List<string>();
    private int spriteIndex = 0;
    private bool destructable = false;
    private bool showedDialogue = false;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Item item;
    [SerializeField] private SubtitleManager subtitleManager;
    [SerializeField] private DialogueSystemTrigger trigger;
    void OnEnable()
    {
        SubtitleManager.OnChangedFromSubtitleToPlaying += OnChangedFromSubtitleToPlaying;
    }

    void OnDisable()
    {
        SubtitleManager.OnChangedFromSubtitleToPlaying -= OnChangedFromSubtitleToPlaying;
    }

    private void OnChangedFromSubtitleToPlaying(string id)
    {
        if (id == "D11")
            this.destructable = true;
    }

    void Start()
    {
        this.subtitles.Add("电视屏幕碎掉了一块");
        this.subtitles.Add("电视屏幕碎掉了一块");
    }

    void Update()
    {
        if (this.spriteIndex >= this.sprites.Count)
        {
            if (this.showedDialogue)
                return;
            this.trigger.OnUse();
            showedDialogue = true;
            return;
        }
        if (!this.destructable)
            return;
        if (!Scene2Manager.hammerGained)
            return;
        if (!Scene2Manager.interactable)
            return;
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            this.renderer.sprite = this.sprites[this.spriteIndex];
            this.subtitleManager.StartDisplaying(this.subtitles.ToArray(), "D11Des");
            this.spriteIndex++;
        }
    }
}
