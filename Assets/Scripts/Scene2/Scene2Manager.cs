using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2Manager : TheSceneManager
{
    [SerializeField] private GameObject walletHammer;
    [SerializeField] private GameObject photoHammer;
    [SerializeField] private GameObject Wallet;
    [SerializeField] private GameObject Photo;
    [SerializeField] private GameObject Hammer;
    [SerializeField] private GameObject Empty;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private SubtitleManager subtitleManager;
    [SerializeField] private DialogueSystemTrigger C09;
    

    public static bool hammerGained = false;
    private bool PhotoGained = false;
    private bool walletGained = false;
    private bool isClear = false;

    void OnEnable()
    {
        SubtitleManager.OnChangedFromPlayingToSubtitle += OnChangedFromPlayingToSubtitle;
        SubtitleManager.OnChangedFromSubtitleToPlaying += OnChangedFromSubtitleToPlaying;
        D14.OnHammerGained += OnHammerGained;
    }

    void OnDisable()
    {
        SubtitleManager.OnChangedFromPlayingToSubtitle -= OnChangedFromPlayingToSubtitle;
        SubtitleManager.OnChangedFromSubtitleToPlaying -= OnChangedFromSubtitleToPlaying;
        D14.OnHammerGained -= OnHammerGained;
    }

    public void OnConversationStart()
    {
        talking = true;
    }


    public void OnConversationEnd()
    {
        talking = false;
    }

    public void DisableC09()
    {
        this.C09.trigger = DialogueSystemTriggerEvent.None;
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

        if (id == "wallet")
        {
            this.walletGained = false;
            this.PhotoGained = true;
            if (hammerGained)
            {
                this.walletHammer.SetActive(false);
                this.photoHammer.SetActive(true);
            }
            else
            {
                this.Wallet.SetActive(false);
                this.Photo.SetActive(true);
            }
        }

        if (id == "photo")
        {
            this.PhotoGained = false;
            if (hammerGained)
            {
                this.photoHammer.SetActive(false);
                this.Hammer.SetActive(true);
            }
            else
            {
                this.Photo.SetActive(false);
                this.Empty.SetActive(true);
            }
        }

    }

    public void SetClear()
    {
        this.isClear = true;
        this.bgm.enabled = false;
    }

    private void OnHammerGained()
    {
        hammerGained = true;
    }

    public void DisplayInventory()
    {
        if(this.walletGained && hammerGained)
            this.walletHammer.SetActive(true);
        else if(this.PhotoGained && hammerGained)
            this.photoHammer.SetActive(true);
        else if(this.walletGained)
            this.Wallet.SetActive(true);
        else if (hammerGained)
            this.Hammer.SetActive(true);
        else if (this.PhotoGained)
            this.Photo.SetActive(true);
        else
            this.Empty.SetActive(true);
    }

    public void UseWallet()
    {
        List<string> subtitles = new List<string>();
        subtitles.Add("你看，无论是游戏还是人生都是这样");
        subtitles.Add("真正正确的选项从来不会给你明显的提示");
        subtitles.Add("或许生活才是最大的解谜游戏？");

        this.subtitleManager.StartDisplaying(subtitles.ToArray(), "wallet");
       
    }

    public void UsePhoto()
    {
        List<string> subtitles = new List<string>();
        subtitles.Add("*泛黄的拍立得照片*");
        subtitles.Add("这张照片上记录着刘备、张飞、关羽在桃园结义时的样子");
        subtitles.Add("和你拿来当书签的那张剧照如出一辙");
        subtitles.Add("你看到这张照片的角落被浅浅的写上了“翼德”二字");
        subtitles.Add("看来你随手捡到的钱包是张飞的");
        subtitles.Add("不过似乎也挺正常，张飞在他们兄弟三人中算是文青了");
        subtitles.Add("把照片塞在钱包夹层这样的事情也不奇怪");
        subtitles.Add("但是可千万不要让他知道你捡到了他的钱包哦");

        this.subtitleManager.StartDisplaying(subtitles.ToArray(), "photo");
        ConditionController.isA07Unlocked = true;
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
        this.walletGained = true;
        this.movable = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isClear)
        {
            if (other.CompareTag("Player"))
            {
                this.LoadScene2_5();
            }
        }
    }
    void LoadScene2_5()
    {
        SceneManager.LoadScene("Scene2.5");
    }

    void Update()
    {
        
    }

}
