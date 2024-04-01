using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static D02;
using static SubtitleManager;

public class Scene0Manager : TheSceneManager
{
    [SerializeField] private GameObject empty;
    [SerializeField] private GameObject accessory;
    [SerializeField] private DialogueDatabase db;
    private bool isD02Acquired = false;

    void OnEnable()
    {
        SubtitleManager.OnChangedFromPlayingToSubtitle += OnChangedFromPlayingToSubtitle;
        SubtitleManager.OnChangedFromSubtitleToPlaying += OnChangedFromSubtitleToPlaying;
        D02.OnD02Acquired += OnD02Acquired;
        N01.OnAccessoryRetrived += OnAccessoryRetrived;
    }

    void OnDisable()
    {
        SubtitleManager.OnChangedFromPlayingToSubtitle -= OnChangedFromPlayingToSubtitle;
        SubtitleManager.OnChangedFromSubtitleToPlaying -= OnChangedFromSubtitleToPlaying;
        D02.OnD02Acquired -= OnD02Acquired;
        N01.OnAccessoryRetrived += OnAccessoryRetrived;
    }
    private void OnAccessoryRetrived()
    {
        this.isD02Acquired = false;
    }

    void OnD02Acquired()
    {
        this.isD02Acquired = true;
    }

    public void OnConversationStart()
    {
        talking = true;
    }


    public void OnConversationEnd()
    {
        talking = false;
    }

    public void DisplayInventory()
    {
        if(this.isD02Acquired)
            this.accessory.SetActive(true);
        else
        {
            this.empty.SetActive(true);
        }
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
        DialogueManager.databaseManager.DefaultDatabase = db;
        Debug.Log("Scene0Manager Start");
        //Invoke("LoadScene1", 5.0f);
        movable = true;
        interactable = true;
        subtittleTriggerable = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Scene0Cleared)
        {
            if (other.CompareTag("Player"))
            {
                this.LoadScene1();
            }
        }
    }
    void LoadScene1()
    {
        Debug.Log("Invoked");
        SceneManager.LoadScene("Scene1");
    }

}
