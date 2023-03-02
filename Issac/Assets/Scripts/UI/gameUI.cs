using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameUI : MonoBehaviour
{
    public GameObject minimap;
    public TMP_Text[] status;
    public Image[] rooms;
    private RectTransform imgrect;
    public GameObject ItemImg;
    private Rigidbody2D itemImgRigid;
    private RectTransform itemImgRect;
    bool move_Chk = false;
    private RectTransform TextBoxSize;
    private TMP_Text TextSize;
    private int miniMapSize;
    private GameObject[,] nowState;
    public Image[] hpImage;
    public Sprite[] hpSprite;
    int checkHp;
    float totalHp;
    bool checkBossHp;
    bool totalHpChange;
    float remainder;
    public Image BossHpBar;
    public Image BossHpBarFill;
    public GameObject nowMap;
    private Image[,] miniMapImage;
    public Image nullImage;
    public GameObject editMode;
    // Start is called before the first frame update
    private void OnEnable()
    {

        miniMapSize = 20 - GameManager.instance.stageNum * 2;
        minimap.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(miniMapSize * (GameManager.instance.stageNum + 7) * 2, miniMapSize * (GameManager.instance.stageNum + 7) * 2);
        minimap.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((-miniMapSize * (GameManager.instance.stageNum + 7)), (-(miniMapSize * (GameManager.instance.stageNum + 7))));
        itemImgRigid = ItemImg.GetComponent<Rigidbody2D>();
        itemImgRect = ItemImg.GetRect();
        TextBoxSize = status[3].GetComponent<RectTransform>();
        TextSize = status[3].GetComponent<TMP_Text>();
        checkBossHp = false;


    }
    void Start()
    {
        totalHp = GameManager.instance.player_Stat.NormalHeart + GameManager.instance.player_Stat.SoulHeart;
        HpController();
        miniMapImage = new Image[GameManager.instance.stageNum + 6, GameManager.instance.stageNum + 6];
        MiniMap();

    }

    // Update is called once per frame
    void Update()
    {
        BossHpUI();
        if (GameManager.instance.player_Stat.NormalHeart == 0 && GameManager.instance.player_Stat.SoulHeart == 0)
        { hpImage[0].sprite = hpSprite[2]; }
        if (GameManager.instance.player_Stat.NormalHeart + GameManager.instance.player_Stat.SoulHeart < totalHp
        || GameManager.instance.player_Stat.NormalHeart + GameManager.instance.player_Stat.SoulHeart > totalHp)
        {
            totalHp = GameManager.instance.player_Stat.NormalHeart + GameManager.instance.player_Stat.SoulHeart;
            totalHpChange = true;
        }
        if (totalHpChange)
        {
            totalHpChange = false;
            HpController();


        }
        status[0].text = $"{GameManager.instance.player_Stat.CoinCount}";
        status[1].text = $"{GameManager.instance.player_Stat.BoomCount}";
        status[2].text = $"{GameManager.instance.player_Stat.KeyCount}";
        status[3].text = $"{GameManager.instance.itemName}";



        if (itemImgRect.anchoredPosition.x > 0 && move_Chk)
        {
            Debug.Log(itemImgRect.anchoredPosition);
            itemImgRigid.velocity = new Vector2(0, 0);

            ItemImg.transform.SetLocalPositionAndRotation(new Vector2(0, ItemImg.transform.localPosition.y), new Quaternion(0f, 0f, 0f, 0f));
            itemImgRect.sizeDelta = new Vector2(500, 100);
            TextBoxSize.sizeDelta = new Vector2(500, 100);
            TextSize.fontSize = 78f;
        }


        if (GameManager.instance.itemgetChk)
        {
            GameManager.instance.itemgetChk = false;
            StartCoroutine(ItemText());
        }
        if (GameManager.instance.roomChange)
        {
            MiniMapChager();

        }


        if (editMode.gameObject.activeSelf)
        {
            if (editMode.GetComponent<TMP_InputField>().text == "1")
            {
                GameManager.instance.player_Stat.Str += 5;
                editMode.GetComponent<TMP_InputField>().text = "";
            }
            else if (editMode.GetComponent<TMP_InputField>().text == "2")
            {
                GameManager.instance.player_Stat.RateSpeed += 3;
                editMode.GetComponent<TMP_InputField>().text = "";
            }
            else if (editMode.GetComponent<TMP_InputField>().text == "Boom")
            {
                GameManager.instance.player_Stat.BoomCount += 5;
                editMode.GetComponent<TMP_InputField>().text = "";
            }
            else if (editMode.GetComponent<TMP_InputField>().text == "Key")
            {
                GameManager.instance.player_Stat.KeyCount += 5;
                editMode.GetComponent<TMP_InputField>().text = "";
            }
            else if (editMode.GetComponent<TMP_InputField>().text == "3")
            {
                GameManager.instance.player_Stat.MaxHp += 4;
                GameManager.instance.player_Stat.NormalHeart += 4;
                editMode.GetComponent<TMP_InputField>().text = "";
            }
            else if (editMode.GetComponent<TMP_InputField>().text == "4")
            {
                GameManager.instance.player_Stat.SoulHeart += 4;
                editMode.GetComponent<TMP_InputField>().text = "";
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                editMode.SetActive(false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("What");
                editMode.SetActive(true);
            }

        }


    }
    IEnumerator ItemText()
    {
        ItemImg.SetActive(true);
        move_Chk = true;
        itemImgRect.sizeDelta = new Vector2(200, 100);
        TextBoxSize.sizeDelta = new Vector2(200, 100);
        TextSize.fontSize = 40f;
        ItemImg.transform.SetLocalPositionAndRotation(new Vector2(-800, ItemImg.transform.localPosition.y), new Quaternion(0f, 0f, 0f, 0f));
        itemImgRigid.velocity = new Vector2(1500, 0);

        yield return new WaitForSeconds(1.5f);
        move_Chk = false;
        ItemImg.transform.SetLocalPositionAndRotation(new Vector2(-12.0f, ItemImg.transform.localPosition.y), new Quaternion(0f, 0f, 0f, 0f));
        itemImgRect.sizeDelta = new Vector2(300, 100);
        itemImgRigid.velocity = new Vector2(1500, 0);
        TextBoxSize.sizeDelta = new Vector2(300, 100);
        TextSize.fontSize = 40f;
        yield return new WaitForSeconds(0.5f);
        ItemImg.SetActive(false);



    }
    void MiniMap()
    {



        for (int i = 0; i < GameManager.instance.stageNum + 6; i++)
        {
            for (int j = 0; j < GameManager.instance.stageNum + 6; j++)
            {
                // if (nowState[j, i] == nowMap)
                // {
                //     if(nowState[j,i+1].GetComponent<DoorController>().roomType == 3 ){

                //     } 
                // }
                switch (GameManager.instance.map[j, i])
                {

                    case 1:
                        Image makeImg = Instantiate(rooms[1]);
                        makeImg.transform.SetParent(minimap.transform, false);
                        makeImg.gameObject.SetActive(true);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        imgrect.transform.SetLocalPositionAndRotation(new Vector2(j * miniMapSize, i * miniMapSize), new Quaternion(0, 0, 0, 0));
                        miniMapImage[j, i] = makeImg;

                        break;
                    case 2:
                        makeImg = Instantiate(rooms[2]);
                        makeImg.transform.SetParent(minimap.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        makeImg.gameObject.SetActive(false);
                        imgrect.transform.SetLocalPositionAndRotation(new Vector2(j * miniMapSize, i * miniMapSize), new Quaternion(0, 0, 0, 0));
                        miniMapImage[j, i] = makeImg;

                        break;
                    case 3:
                        makeImg = Instantiate(rooms[3]);
                        makeImg.transform.SetParent(minimap.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        makeImg.gameObject.SetActive(false);
                        imgrect.transform.SetLocalPositionAndRotation(new Vector2(j * miniMapSize, i * miniMapSize), new Quaternion(0, 0, 0, 0));
                        miniMapImage[j, i] = makeImg;

                        break;
                    case 4:
                        makeImg = Instantiate(rooms[4]);
                        makeImg.transform.SetParent(minimap.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        makeImg.gameObject.SetActive(false);
                        miniMapImage[j, i] = makeImg;
                        imgrect.transform.SetLocalPositionAndRotation(new Vector2(j * miniMapSize, i * miniMapSize), new Quaternion(0, 0, 0, 0));

                        break;
                    case 5:
                        makeImg = Instantiate(rooms[5]);
                        makeImg.transform.SetParent(minimap.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        makeImg.gameObject.SetActive(false);
                        miniMapImage[j, i] = makeImg;
                        imgrect.transform.SetLocalPositionAndRotation(new Vector2(j * miniMapSize, i * miniMapSize), new Quaternion(0, 0, 0, 0));

                        break;
                    case 6:
                        makeImg = Instantiate(rooms[6]);
                        makeImg.transform.SetParent(minimap.transform, false);
                        imgrect = makeImg.GetComponent<RectTransform>();
                        makeImg.gameObject.SetActive(false);
                        miniMapImage[j, i] = makeImg;
                        imgrect.transform.SetLocalPositionAndRotation(new Vector2(j * miniMapSize, i * miniMapSize), new Quaternion(0, 0, 0, 0));

                        break;
                    default:
                        miniMapImage[j, i] = nullImage;
                        break;

                }


            }
        }
    }
    void HpController()
    {
        int checkHp;

        int MaxHp;

        int checkNormalHeart;
        int checkSoulHeart;
        int nomal_Soul;
        if (GameManager.instance.player_Stat.NormalHeart == 0 && GameManager.instance.player_Stat.SoulHeart == 0)
        { }
        else
        {
            for (int i = 0; i < hpImage.Length; i++)
            {
                hpImage[i].transform.gameObject.SetActive(false);
            }
            MaxHp = (int)GameManager.instance.player_Stat.MaxHp;
            checkNormalHeart = (int)GameManager.instance.player_Stat.NormalHeart;
            checkSoulHeart = (int)GameManager.instance.player_Stat.SoulHeart;

            checkHp = 0;
            nomal_Soul = 0;
            while (MaxHp > 0)
            {
                MaxHp -= 2;
                checkHp++;
                nomal_Soul++;
            }


            for (int i = 0; i < checkHp; i++)
            {
                hpImage[i].transform.gameObject.SetActive(true);

                if ((int)GameManager.instance.player_Stat.MaxHp == checkNormalHeart)
                {
                    hpImage[i].sprite = hpSprite[0];
                }
                else
                {
                    hpImage[i].sprite = hpSprite[2];
                }

            }
            if ((int)GameManager.instance.player_Stat.MaxHp == checkNormalHeart)
            {

            }
            else
            {
                checkHp = 0;
                remainder = checkNormalHeart % 2;
                while (checkNormalHeart > 0)
                {

                    checkNormalHeart -= 2;
                    checkHp++;

                }
                if (checkHp == 0)
                {
                    hpImage[0].sprite = hpSprite[1];
                }
                for (int i = 0; i < checkHp; i++)
                {
                    hpImage[i].transform.gameObject.SetActive(true);
                    hpImage[i].sprite = hpSprite[0];
                    if (remainder == 1 && i == checkHp - 1)
                    {
                        hpImage[i].sprite = hpSprite[1];
                    }

                }
            }

            checkHp = 0;
            remainder = checkSoulHeart % 2;
            // Debug.Log($"soul: {checkSoulHeart}");
            while (checkSoulHeart > 0)
            {

                checkSoulHeart -= 2;

                checkHp++;
            }
            for (int i = nomal_Soul; i < nomal_Soul + checkHp; i++)
            {
                if (i > hpImage.Length - 1)
                {

                }
                else
                {

                    hpImage[i].transform.gameObject.SetActive(true);
                    hpImage[i].sprite = hpSprite[3];

                    // Debug.Log($"soul Number: {i}");s
                    if (i == nomal_Soul + checkHp - 1 && remainder == 1f && i + 1 < hpImage.Length)
                    {
                        hpImage[i].sprite = hpSprite[4];
                    }
                }
            }
            Debug.Log(GameManager.instance.player_Stat.NormalHeart);
            Debug.Log(GameManager.instance.player_Stat.SoulHeart);
        }



    }
    void BossHpUI()
    {
        int bossMaxHp;

        if (GameManager.instance.bossType == 1)
        {
            bossMaxHp = 120;
        }
        else
        {
            bossMaxHp = 200;
        }
        if (GameManager.instance.bossCheck)
        {
            BossHpBar.gameObject.SetActive(true);
            BossHpBarFill.fillAmount = GameManager.instance.bossHp / bossMaxHp;
            if (GameManager.instance.monsterHit)
            {

                StartCoroutine(BossHpBlink());
            }
        }
        else
        {
            BossHpBar.gameObject.SetActive(false);
        }
    }

    void MiniMapChager()
    {
        nowState = GameManager.instance.nowMapStat;

        nowMap = GameManager.instance.NowMap;

        for (int i = 0; i < GameManager.instance.stageNum + 6; i++)
        {
            for (int j = 0; j < GameManager.instance.stageNum + 6; j++)
            {
                if (nowState[j, i] == nowMap)
                {

                    if (i >= 0 && i + 1 < GameManager.instance.stageNum + 6 && j >= 0 && j + 1 < GameManager.instance.stageNum + 6)
                    {

                        if (i + 1 <= GameManager.instance.stageNum + 6 && miniMapImage[j, i + 1] != nullImage)
                        {
                            miniMapImage[j, i + 1].gameObject.SetActive(true);
                            // Debug.Log(miniMapImage[j, i + 1].name);
                        }
                        if (j + 1 <= GameManager.instance.stageNum + 6 && miniMapImage[j + 1, i] != nullImage)
                        {
                            miniMapImage[j + 1, i].gameObject.SetActive(true);
                            // Debug.Log(miniMapImage[j + 1, i].name);
                        }
                        if (j - 1 >= 0 && miniMapImage[j - 1, i] != nullImage)
                        {
                            miniMapImage[j - 1, i].gameObject.SetActive(true);
                            // Debug.Log(miniMapImage[j - 1, i].name);
                        }
                        if (i - 1 >= 0 && miniMapImage[j, i - 1] != nullImage)
                        {
                            miniMapImage[j, i - 1].gameObject.SetActive(true);
                            // Debug.Log(miniMapImage[j, i - 1].name);
                        }
                    }
                }
            }
        }
    }
    IEnumerator BossHpBlink()
    {
        GameManager.instance.monsterHit = false;
        BossHpBarFill.color = new Color(0.8f, 0, 0, 1);
        yield return new WaitForSeconds(0.03f);
        BossHpBarFill.color = new Color(0.4f, 0, 0, 1);
        yield return new WaitForSeconds(0.03f);
        BossHpBarFill.color = new Color(0f, 0, 0, 1);
        yield return new WaitForSeconds(0.03f);
        BossHpBarFill.color = new Color(0.4f, 0, 0, 1);
        yield return new WaitForSeconds(0.03f);
        BossHpBarFill.color = new Color(0.8f, 0, 0, 1);
        yield return new WaitForSeconds(0.03f);
        BossHpBarFill.color = new Color(0.8f, 0, 0, 1);
        yield return new WaitForSeconds(0.03f);
        BossHpBarFill.color = new Color(0.4f, 0, 0, 1);
        yield return new WaitForSeconds(0.03f);
        BossHpBarFill.color = new Color(0f, 0, 0, 1);
        yield return new WaitForSeconds(0.03f);
        BossHpBarFill.color = new Color(0.4f, 0, 0, 1);
        yield return new WaitForSeconds(0.03f);
        BossHpBarFill.color = new Color(0.8f, 0, 0, 1);
        yield return new WaitForSeconds(0.03f);


    }
}
