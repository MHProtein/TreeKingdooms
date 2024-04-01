using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene4Manager : TheSceneManager
{
    [TextArea] public List<string> A09;
    [TextArea] public List<string> A10;
    [SerializeField] private SubtitleManager subtitleManager;

    [SerializeField] private GameObject ui;
    [SerializeField] private Text text;

    void OnEnable()
    {
        SubtitleManager.OnChangedFromPlayingToSubtitle += OnChangedFromPlayingToSubtitle;
        SubtitleManager.OnChangedFromSubtitleToPlaying += OnChangedFromSubtitleToPlaying;
    }

    void OnDisable()
    {
        SubtitleManager.OnChangedFromPlayingToSubtitle -= OnChangedFromPlayingToSubtitle;
        SubtitleManager.OnChangedFromSubtitleToPlaying -= OnChangedFromSubtitleToPlaying;

    }

    private void OnChangedFromPlayingToSubtitle(string id)
    {
        currentState = GameState.SUBTITLE;
        movable = false;
        interactable = false;
        subtittleTriggerable = false;
        Talkable = false;
    }

    private void OnChangedFromSubtitleToPlaying(string id)
    {
        currentState = GameState.PLAYING;
        Invoke("ChangeBooleans", 0.5f);

        if (id == "A09" || id == "A10")
        {
            SceneManager.LoadScene("StartUI");
        } 
    }

    void Start()
    {
        this.ui.SetActive(true);
        this.text.enabled = true;
        if (ConditionController.isA07Unlocked)
        {
            this.subtitleManager.StartDisplaying(this.A10.ToArray(), "A10");
            Debug.Log("A10");
        }
        else
        {
            this.subtitleManager.StartDisplaying(this.A09.ToArray(), "A09");
            Debug.Log("A9");
        }
    }
}
