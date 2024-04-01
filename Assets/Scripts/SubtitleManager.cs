using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class SubtitleManager : MonoBehaviour
{
    private Queue<string> queue = new Queue<string>();
    private bool playing;
    private string currentID;

    [SerializeField] private GameObject ui;
    [SerializeField] private Text text;

    public delegate void onChangedFromPlayingToSubtitle(string id);
    public static event onChangedFromPlayingToSubtitle OnChangedFromPlayingToSubtitle;

    public delegate void onChangedFromSubtitleToPlaying(string id);
    public static event onChangedFromSubtitleToPlaying OnChangedFromSubtitleToPlaying;

    void Start()
    {
        this.ui.SetActive(false);
    }

    void Update()
    {
        if (!this.playing)
            return;
        if (Keyboard.current.fKey.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame)
        {
            this.NextSubtitle();
        }
    }

    public void StartDisplaying(string subtitle, string id)
    {
        this.queue.Enqueue(subtitle);

        currentID = id;
        this.playing = true;
        OnChangedFromPlayingToSubtitle?.Invoke(id);

        if (!this.ui.activeSelf)
        {
            this.ui.SetActive(true);
            this.text.enabled = true;
        }
        this.NextSubtitle();
    }

    public void StartDisplaying(string[] subtitles, string id)
    {
        foreach (var subtitle in subtitles)
        {
            this.queue.Enqueue(subtitle);
        }

        currentID = id;
        this.playing = true;
        OnChangedFromPlayingToSubtitle?.Invoke(id);

        if (!this.ui.activeSelf)
        {
            this.ui.SetActive(true);
            this.text.enabled = true;
        }
        this.NextSubtitle();
    }

    void NextSubtitle()
    {
        if (this.queue.Count == 0 && this.playing)
        {
            this.playing = false;
            this.EndDisplaying();
            return;
        }
        text.text = Convert.ToString(this.queue.Dequeue());
    }

    void EndDisplaying()
    {
        OnChangedFromSubtitleToPlaying?.Invoke(this.currentID);
        this.ui.SetActive(false);
        this.text.enabled = false;
    }

}
