using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Manager : TheSceneManager
{

    [SerializeField] private AudioSource M1;
    [SerializeField] private AudioSource M2;
    [SerializeField] private GameObject guanYu;

    [SerializeField] private GameObject empty;
    [SerializeField] private GameObject wallet;
    [SerializeField] private GameObject both;

    [SerializeField] private SubtitleManager subtitleManager;

    private int radioUseCount = 0;
    private List<string> subtitles = new List<string>();

    public static bool WalletGained = false;
    public static bool RadioGained = false;
    public static bool conversedWithSoilder = false;
    public static bool isClear = false;
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

        if (id == "D06")
        {
            this.M1.Play();
            Invoke("EnableGuanYu", 20.0f);
        }

        if (id == "EndingSubtitles")
        {
            isClear = true;
        }
    }

    public void OpenInventory()
    {
        Debug.Log("WalletGained : " + WalletGained);
        Debug.Log("RadioGained : " + RadioGained);
        
        if (WalletGained && RadioGained)
            this.both.SetActive(true);
        else if (WalletGained)
            this.wallet.SetActive(true);
        else
            this.empty.SetActive(true);
    }

    public void IncrementRadioCount()
    {
        this.radioUseCount++;
        if (this.radioUseCount > 5)
        {
            this.M2.Play();
            Invoke("EndingSubtitle", 3.238f);
        }
    }

    void EndingSubtitle()
    {
        this.subtitleManager.StartDisplaying(this.subtitles.ToArray(), "EndingSubtitles");
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
        //Invoke("LoadScene1", 5.0f);
        movable = true;
        interactable = true;
        subtittleTriggerable = true;

        this.subtitles.Add("“这是什么鬼动静？”你不禁在心里发出这样的感叹。");
        this.subtitles.Add("此时让你感到崩溃的，并非如此光怪陆离的世界");
        this.subtitles.Add("而是在这样的世界中，原本的人物形象也被理所当然地歪曲了");
        this.subtitles.Add("你尝试用认知滤网来欺骗自己");
        this.subtitles.Add("宁愿归因于自己也不愿质疑你所相信的");
        this.subtitles.Add("然而你现在不得不相信，这个世界的确是扭曲后的");
        this.subtitles.Add("与此同时，你身为故事的主角");
        this.subtitles.Add("下定了决心去纠正这些扭曲——就从三英战吕布开始");

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isClear)
        {
            if (other.CompareTag("Player"))
            {
                this.LoadScene2();
            }
        }
    }
    void LoadScene2()
    {
        Debug.Log("Invoked");
        SceneManager.LoadScene("Scene2");
    }

    void EnableGuanYu()
    {
        this.guanYu.SetActive(true);
    }

}
