using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player_Stat player_Stat;
    public bool itemgetChk;
    public string itemName;
    public List<int> totalItem = new List<int>();
    public List<Charator> player = new List<Charator>();
    public bool monsterClearChk = false;
    public int stageNum = 1;
    public int[,] map;
    public GameObject[,] nowMapStat;
    public GameObject NowMap;
    public bool roomChange;
    public int monsterCount;
    public float bossHp = 0;
    public int bossType = 1;
    public bool bossCheck = false;
    public bool monsterHit = false;
    public bool MainUiActive = false;
    public int choiceCharator = 1;
    public int sfx = 5;
    public int music = 5;
    public bool SceneChanger;
    public bool introSkip;
    public Canvas enddingVideo;
    public bool bossEnter;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Coroutine StartCoroutineDeligate(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }
}
