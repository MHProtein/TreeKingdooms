using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using static D14;

public class Scene3Manager : TheSceneManager
{

    public static bool D16Interected;
    public static bool D18Interected;
    public static bool D19Interected;
    public static bool talked;
    public static bool isClear;

    [SerializeField] DialogueSystemTrigger C13;
    [SerializeField] DialogueSystemTrigger C12;

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

    public void OnConversationStart()
    {
        talking = true;
    }

    public void OnConversationEnd()
    {
        talking = false;
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

        if (id == "D18")
            D18Interected = true;
        if (id == "D16")
        {
            D16Interected = true;
            this.C12.OnUse();
        }

        if (id == "D19")
        {
            D19Interected = true;
            this.C13.OnUse();
        }
    }

    public void SetClear()
    {
        isClear = true;
    }

    public void SetTalked()
    {
        talked = true;
    }

    void ChangeBooleans()
    {
        movable = true;
        interactable = true;
        subtittleTriggerable = true;
        Talkable = true;
    }

    void Start()
    {
        this.movable = true;
    }

  
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isClear)
        {
            if (other.CompareTag("Player"))
            {
                this.LoadScene4();
            }
        }
    }
    void LoadScene4()
    {
        SceneManager.LoadScene("Scene4");
    }

}
